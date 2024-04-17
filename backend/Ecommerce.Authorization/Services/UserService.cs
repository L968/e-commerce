using System.Transactions;
using System.Web;

namespace Ecommerce.Authorization.Services;

public class UserService(
    UserManager<CustomIdentityUser> userManager,
    IEmailService emailService,
    ISmsService smsService
    ) : IUserService
{
    private readonly UserManager<CustomIdentityUser> _userManager = userManager;
    private readonly IEmailService _emailService = emailService;
    private readonly ISmsService _smsService = smsService;

    public Result CreateUser(CreateUserDto createUserDto)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var identityUser = new CustomIdentityUser()
        {
            UserName = createUserDto.Email!,
            Name = createUserDto.Name!,
            Email = createUserDto.Email!
        };

        var identityResult = _userManager.CreateAsync(identityUser, createUserDto.Password).Result;

        if (!identityResult.Succeeded) return Result.Fail(identityResult.Errors.Where(error => !error.Description.Contains("Username")).FirstOrDefault()!.Description);

        _userManager.AddToRoleAsync(identityUser, "regular");

        string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;
        string encodedToken = HttpUtility.UrlEncode(confirmationToken);
        _emailService.SendEmailConfirmationEmail(identityUser.Email, identityUser.Id, encodedToken);

        transaction.Complete();
        return Result.Ok();
    }

    public Result UpdatePhoneNumber(int userId, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return Result.Fail("Phone number is required");

        bool IsPhoneAlreadyRegistered = _userManager.Users.Any(user => user.PhoneNumber == phoneNumber);
        if (IsPhoneAlreadyRegistered) return Result.Fail("Phone number is already taken");

        var identityUser = _userManager.Users.FirstOrDefault(user => user.Id == userId);
        if (identityUser is null) return Result.Fail("Error in updating phone number");

        string confirmationToken = _userManager.GenerateChangePhoneNumberTokenAsync(identityUser, phoneNumber).Result;
        _smsService.SendPhoneNumberConfirmationSms(phoneNumber, confirmationToken);

        return Result.Ok();
    }

    public Result UpdateTwoFactorAuthentication(int userId, bool twoFactorEnabled)
    {
        var identityUser = _userManager.Users.FirstOrDefault(user => user.Id == userId);
        if (identityUser is null) return Result.Fail("Error in updating two factor authentication");

        if (twoFactorEnabled && !identityUser.PhoneNumberConfirmed) return Result.Fail("You must have a confirmed phone number in order to activate two factor authentication");

        identityUser.TwoFactorEnabled = twoFactorEnabled;
        var result = _userManager.UpdateAsync(identityUser).Result;

        return Result.Ok();
    }

    public Result ConfirmPhoneNumber(int userId, string phoneNumber, string confirmationToken)
    {
        var identityUser = _userManager.Users.FirstOrDefault(user => user.Id == userId);
        if (identityUser is null) return Result.Fail("Error in confirming phone number");

        var result = _userManager.ChangePhoneNumberAsync(identityUser, phoneNumber, confirmationToken).Result;

        if (!result.Succeeded) return Result.Fail(result.Errors.FirstOrDefault()!.Code);

        return Result.Ok();
    }

    public Result ActivateUser(ActivateUserRequest activateUserRequest)
    {
        var identityUser = _userManager.Users.FirstOrDefault(user => user.Id == activateUserRequest.Id);
        if (identityUser is null) return Result.Fail("Error in activating user");

        var identityResult = _userManager.ConfirmEmailAsync(identityUser, activateUserRequest.ConfirmationCode).Result;
        if (!identityResult.Succeeded) return Result.Fail("Error in activating user");

        return Result.Ok();
    }

    public async Task<Guid?> GetDefaultAddressId(int userId)
    {
        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
        if (identityUser is null) return null;

        return identityUser.DefaultAddressId;
    }

    public async Task<Result> UpdateDefaultAddress(Guid? addressId, int userId)
    {
        if (addressId is not null)
        {
            if (addressId == Guid.Empty) return Result.Fail("Invalid address id value");
        }

        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
        if (identityUser is null) return Result.Fail("User not found");

        identityUser.DefaultAddressId = addressId;
        await _userManager.UpdateAsync(identityUser);

        return Result.Ok();
    }
}
