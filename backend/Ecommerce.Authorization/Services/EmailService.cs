using MailKit.Net.Smtp;
using MimeKit;

namespace Ecommerce.Authorization.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailConfirmationEmail(string to, int userId, string confirmationToken)
    {
        var mailboxAdressess = new List<MailboxAddress> { new("", to) };

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("", Config.EmailSettingsFrom));
        emailMessage.To.AddRange(mailboxAdressess);
        emailMessage.Subject = "Verify your new Ecommerce account";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = $"To verify your email address, please use the following link: \n\nhttps://localhost:7252/user/activate?id={userId}&confirmationCode={confirmationToken}"
        };

        await Send(emailMessage);
    }

    public async Task SendResetPasswordEmail(string to, string passwordResetToken)
    {
        var mailboxAdressess = new List<MailboxAddress> { new("", to) };

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("", Config.EmailSettingsFrom));
        emailMessage.To.AddRange(mailboxAdressess);
        emailMessage.Subject = "Reset your Ecommerce password";

        string htmlBody = Properties.Resources.reset_password_email.Replace("{resetPasswordUrl}", $"https://localhost:7252/reset-password/{passwordResetToken}");
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = htmlBody;

        emailMessage.Body = bodyBuilder.ToMessageBody();

        await Send(emailMessage);
    }

    private static async Task Send(MimeMessage emailMessage)
    {
        using var client = new SmtpClient();

        client.Connect(Config.EmailSettingsSmtpServer, Config.EmailSettingsPort, true);
        client.AuthenticationMechanisms.Remove("XOUATH2");
        client.Authenticate(Config.EmailSettingsFrom, Config.EmailSettingsPassword);

        await client.SendAsync(emailMessage);
    }
}
