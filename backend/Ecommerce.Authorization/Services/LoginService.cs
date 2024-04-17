namespace Ecommerce.Authorization.Services;

public class LoginService(
    SignInManager<CustomIdentityUser> signInManager,
    TokenService tokenService,
    EmailService emailService,
    SmsService smsService
    )
{
    private readonly SignInManager<CustomIdentityUser> _signInManager = signInManager;
    private readonly TokenService _tokenService = tokenService;
    private readonly EmailService _emailService = emailService;
    private readonly SmsService _smsService = smsService;

    public Result Login(LoginRequest loginRequest)
    {
        var identityUser = GetIdentityUserByEmailOrPhoneNumber(loginRequest.EmailOrPhoneNumber!);
        if (identityUser == null) return Result.Fail("Your login credentials don't match an account in our system");

        var signInResult = _signInManager.PasswordSignInAsync(identityUser, loginRequest.Password, false, true).Result;

        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed) return Result.Fail("Email isn't confirmed");
            if (signInResult.IsLockedOut) return Result.Fail("User is currently locked out");

            if (signInResult.RequiresTwoFactor)
            {
                if (string.IsNullOrWhiteSpace(identityUser.PhoneNumber)) return Result.Fail("Two factor login requires phoneNumber");

                var twoFactorToken = _signInManager.UserManager.GenerateTwoFactorTokenAsync(identityUser, "Phone").Result;
                _smsService.SendTwoFactorTokenSms(identityUser.PhoneNumber, twoFactorToken);
                return Result.Fail("User requires two factor authentication");
            }

            return Result.Fail("Your login credentials don't match an account in our system");
        }

        var role = _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
        var token = _tokenService.CreateToken(identityUser, role!);

        return Result.Ok().WithSuccess(token.Value);
    }

    public Result TwoFactorLogin(TwoFactorLoginRequest twoFactorLoginRequest)
    {
        var identityUser = _signInManager.UserManager.Users.FirstOrDefault(user => user.Id == twoFactorLoginRequest.UserId);
        if (identityUser == null) return Result.Fail("Error in two factor login");

        var signInResult = _signInManager.TwoFactorSignInAsync(
            "Phone",
            twoFactorLoginRequest.TwoFactorToken,
            false,
            true
        ).Result;

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

        var role = _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
        var token = _tokenService.CreateToken(identityUser, role!);

        return Result.Ok().WithSuccess(token.Value);
    }

    public Result RequestPasswordReset(RequestPasswordResetRequest request)
    {
        var identityUser = GetIdentityUserByEmail(request.Email!);
        if (identityUser == null) return Result.Ok();

        string passwordResetToken = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
        _emailService.SendResetPasswordEmail(request.Email!, passwordResetToken);

        return Result.Ok();
    }

    public Result PasswordReset(PasswordResetRequest request)
    {
        var identityUser = GetIdentityUserByEmail(request.Email!);
        if (identityUser == null) return Result.Fail("Error in password reset");

        var identityResult = _signInManager
            .UserManager
            .ResetPasswordAsync(identityUser, request.Token, request.Password)
            .Result;

        return identityResult.Succeeded
            ? Result.Ok()
            : Result.Fail("Error in password reset");
    }

    private CustomIdentityUser? GetIdentityUserByEmail(string email)
    {
        return _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
    }

    private CustomIdentityUser? GetIdentityUserByEmailOrPhoneNumber(string emailOrPhoneNumber)
    {
        return _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user =>
                user.NormalizedEmail == emailOrPhoneNumber.ToUpper() ||
                user.PhoneNumber == emailOrPhoneNumber
            );
    }
}
