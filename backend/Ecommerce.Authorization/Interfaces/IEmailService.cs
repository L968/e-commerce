namespace Ecommerce.Authorization.Interfaces;

public interface IEmailService
{
    void SendEmailConfirmationEmail(string to, int userId, string confirmationToken);
    void SendResetPasswordEmail(string to, string passwordResetToken);
}
