using Ecommerce.Authorization.Data.Requests;

namespace Ecommerce.Authorization.Services
{
    public class LoginService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService, EmailService emailService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public Result Login(LoginRequest loginRequest)
        {
            var identityUser = GetIdentityUserByEmail(loginRequest.Email!);

            if (identityUser == null) return Result.Fail("Your login credentials don't match an account in our system");

            // CheckPasswordSignInAsync
            var signInResult = _signInManager.PasswordSignInAsync(identityUser.UserName, loginRequest.Password, false, true).Result;

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

            if (identityUser == null) return Result.Fail("Email not registered");

            string passwordResetToken = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
            _emailService.SendResetPasswordEmail(request.Email!, passwordResetToken);

            return Result.Ok().WithSuccess(passwordResetToken);
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
    }
}