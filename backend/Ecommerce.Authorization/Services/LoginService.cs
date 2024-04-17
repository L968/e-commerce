namespace Ecommerce.Authorization.Services;

public class LoginService(
    SignInManager<CustomIdentityUser> signInManager,
    ITokenService tokenService,
    IEmailService emailService,
    ISmsService smsService
    ) : ILoginService
{
    private readonly SignInManager<CustomIdentityUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEmailService _emailService = emailService;
    private readonly ISmsService _smsService = smsService;

    public async Task<Result> Login(LoginRequest loginRequest)
    {
        var identityUser = await GetIdentityUserByEmailOrPhoneNumber(loginRequest.EmailOrPhoneNumber!);
        if (identityUser is null)
            return Result.Fail("Your login credentials don't match an account in our system");

        var signInResult = await _signInManager.PasswordSignInAsync(identityUser, loginRequest.Password, false, true);

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed) return Result.Fail("Email isn't confirmed");
            if (signInResult.IsLockedOut) return Result.Fail("User is currently locked out");

            if (signInResult.RequiresTwoFactor)
            {
                if (string.IsNullOrWhiteSpace(identityUser.PhoneNumber)) return Result.Fail("Two factor login requires phoneNumber");

                var twoFactorToken = await _signInManager.UserManager.GenerateTwoFactorTokenAsync(identityUser, "Phone");
                await _smsService.SendTwoFactorTokenSms(identityUser.PhoneNumber, twoFactorToken);
                return Result.Fail("User requires two factor authentication");
            }

            return Result.Fail("Your login credentials don't match an account in our system");
        }

        var roles = await _signInManager.UserManager.GetRolesAsync(identityUser);
        var token = _tokenService.CreateToken(identityUser, roles.FirstOrDefault()!);

        return Result.Ok().WithSuccess(token.Value);
    }

    public async Task<Result> TwoFactorLogin(TwoFactorLoginRequest twoFactorLoginRequest)
    {
        var identityUser = await _signInManager.UserManager.Users.FirstOrDefaultAsync(user => user.Id == twoFactorLoginRequest.UserId);
        if (identityUser is null)
            return Result.Fail("Error in two factor login");

        var signInResult = await _signInManager.TwoFactorSignInAsync(
            "Phone",
            twoFactorLoginRequest.TwoFactorToken,
            false,
            true
        );

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed)
            {
                return Result.Fail("Email isn't confirmed");
            }

            if (signInResult.IsLockedOut)
            {
                return Result.Fail("User is currently locked out");
            }

            return Result.Fail("Your login credentials don't match an account in our system");
        }

        var roles = await _signInManager.UserManager.GetRolesAsync(identityUser);
        var token = _tokenService.CreateToken(identityUser, roles.FirstOrDefault()!);

        return Result.Ok().WithSuccess(token.Value);
    }

    public async Task<Result> RequestPasswordReset(RequestPasswordResetRequest request)
    {
        var identityUser = await GetIdentityUserByEmail(request.Email!);
        if (identityUser is null) return Result.Ok();

        string passwordResetToken = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser);
        await _emailService.SendResetPasswordEmail(request.Email!, passwordResetToken);

        return Result.Ok();
    }

    public async Task<Result> PasswordReset(PasswordResetRequest request)
    {
        var identityUser = await GetIdentityUserByEmail(request.Email!);
        if (identityUser is null) return Result.Fail("Error in password reset");

        var identityResult = await _signInManager
            .UserManager
            .ResetPasswordAsync(identityUser, request.Token, request.Password);

        return identityResult.Succeeded
            ? Result.Ok()
            : Result.Fail("Error in password reset");
    }

    private async Task<CustomIdentityUser?> GetIdentityUserByEmail(string email)
    {
        return await _signInManager
            .UserManager
            .Users
            .FirstOrDefaultAsync(user => user.NormalizedEmail == email.ToUpper());
    }

    private async Task<CustomIdentityUser?> GetIdentityUserByEmailOrPhoneNumber(string emailOrPhoneNumber)
    {
        return await _signInManager
            .UserManager
            .Users
            .FirstOrDefaultAsync(user =>
                user.NormalizedEmail == emailOrPhoneNumber.ToUpper() ||
                user.PhoneNumber == emailOrPhoneNumber
            );
    }
}
