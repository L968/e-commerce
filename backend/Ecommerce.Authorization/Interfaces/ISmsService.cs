namespace Ecommerce.Authorization.Interfaces;

public interface ISmsService
{
    void SendPhoneNumberConfirmationSms(string to, string confirmationToken);
    void SendTwoFactorTokenSms(string to, string twoFactorToken);
}
