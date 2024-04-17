namespace Ecommerce.Authorization.Interfaces;

public interface ILoginService
{
    Result Login(LoginRequest loginRequest);
    Result TwoFactorLogin(TwoFactorLoginRequest twoFactorLoginRequest);
    Result RequestPasswordReset(RequestPasswordResetRequest request);
    Result PasswordReset(PasswordResetRequest request);
}
