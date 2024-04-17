namespace Ecommerce.Authorization.Interfaces;

public interface ISmsService
{
    Task SendPhoneNumberConfirmationSms(string to, string confirmationToken);
    Task SendTwoFactorTokenSms(string to, string twoFactorToken);
}
