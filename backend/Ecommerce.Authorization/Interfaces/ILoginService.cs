namespace Ecommerce.Authorization.Interfaces;

public interface ILoginService
{
    Task<Result> Login(LoginRequest loginRequest);
    Task<Result> TwoFactorLogin(TwoFactorLoginRequest twoFactorLoginRequest);
    Task<Result> RequestPasswordReset(RequestPasswordResetRequest request);
    Task<Result> PasswordReset(PasswordResetRequest request);
}
