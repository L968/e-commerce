using Ecommerce.Authorization.Errors;
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

    public async Task<CustomIdentityUser?> GetUserByIdAsync(int userId)
    {
        return await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<Guid?> GetDefaultAddressIdAsync(int userId)
    {
        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (identityUser is null)
            return null;

        return identityUser.DefaultAddressId;
    }

    public async Task<Result> CreateUserAsync(CreateUserDto createUserDto)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var identityUser = new CustomIdentityUser()
        {
            UserName = createUserDto.Email!,
            Name = createUserDto.Name!,
            Email = createUserDto.Email!
        };

        var identityResult = await _userManager.CreateAsync(identityUser, createUserDto.Password);

        if (!identityResult.Succeeded)
            return Result.Fail(identityResult.Errors.Where(error => !error.Description.Contains("Username")).FirstOrDefault()!.Description);

        await _userManager.AddToRoleAsync(identityUser, "regular");

        string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
        string encodedToken = HttpUtility.UrlEncode(confirmationToken);

        await _emailService.SendEmailConfirmationEmail(identityUser.Email, identityUser.Id, encodedToken);

        transaction.Complete();
        return Result.Ok();
    }

    public async Task<Result> UpdateDefaultAddressAsync(Guid? addressId, int userId)
    {
        if (addressId.HasValue && addressId.Value == Guid.Empty)
            return ServiceErrors.UserService.InvalidAddressIdValue;

        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (identityUser is null)
            return ServiceErrors.NotFound("IdentityUser", userId);

        identityUser.DefaultAddressId = addressId;
        await _userManager.UpdateAsync(identityUser);

        return Result.Ok();
    }

    public async Task<Result> UpdateTwoFactorAuthenticationAsync(int userId, bool twoFactorEnabled)
    {
        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (identityUser is null)
            return ServiceErrors.UserService.ErrorInUpdatingTwoFactorAuthentication;

        if (twoFactorEnabled && !identityUser.PhoneNumberConfirmed)
            return ServiceErrors.UserService.PhoneNumberConfirmationRequiredForTwoFactorActivation;

        identityUser.TwoFactorEnabled = twoFactorEnabled;
        var result = await _userManager.UpdateAsync(identityUser);

        return Result.Ok();
    }

    public async Task<Result> UpdatePhoneNumberAsync(int userId, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return ServiceErrors.UserService.PhoneNumberIsRequired;

        bool IsPhoneAlreadyRegistered = await _userManager.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);

        if (IsPhoneAlreadyRegistered)
            return ServiceErrors.UserService.PhoneNumberIsAlreadyTaken;

        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (identityUser is null)
            return ServiceErrors.UserService.ErrorInUpdatingPhoneNumber;

        string confirmationToken = await _userManager.GenerateChangePhoneNumberTokenAsync(identityUser, phoneNumber);
        await _smsService.SendPhoneNumberConfirmationSms(phoneNumber, confirmationToken);

        return Result.Ok();
    }

    public async Task<Result> ConfirmPhoneNumberAsync(int userId, string phoneNumber, string confirmationToken)
    {
        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (identityUser is null)
            return ServiceErrors.UserService.ErrorInConfirmingPhoneNumber;

        var result = await _userManager.ChangePhoneNumberAsync(identityUser, phoneNumber, confirmationToken);

        if (!result.Succeeded)
            return Result.Fail(result.Errors.FirstOrDefault()!.Code);

        return Result.Ok();
    }

    public async Task<Result> ActivateUserAsync(ActivateUserRequest activateUserRequest)
    {
        var identityUser = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == activateUserRequest.Id);

        if (identityUser is null)
            return ServiceErrors.UserService.ErrorInActivatingUser;

        var identityResult = await _userManager.ConfirmEmailAsync(identityUser, activateUserRequest.ConfirmationCode);
        if (!identityResult.Succeeded)
            return ServiceErrors.UserService.ErrorInActivatingUser;

        return Result.Ok();
    }
}
