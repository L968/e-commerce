namespace Ecommerce.Authorization.Interfaces;

public interface IEmailService
{
    Task SendEmailConfirmationEmail(string to, int userId, string confirmationToken);
    Task SendResetPasswordEmail(string to, string passwordResetToken);
}
