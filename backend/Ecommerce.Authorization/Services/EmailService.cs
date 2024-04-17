using MimeKit;
using MailKit.Net.Smtp;

namespace Ecommerce.Authorization.Services;

public class EmailService
{
    public void SendEmailConfirmationEmail(string to, int userId, string confirmationToken)
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

        Send(emailMessage);
    }

    public void SendResetPasswordEmail(string to, string passwordResetToken)
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

        Send(emailMessage);
    }

    private void Send(MimeMessage emailMessage)
    {
        using var client = new SmtpClient();

        client.Connect(Config.EmailSettingsSmtpServer, Config.EmailSettingsPort, true);
        client.AuthenticationMechanisms.Remove("XOUATH2");
        client.Authenticate(Config.EmailSettingsFrom, Config.EmailSettingsPassword);

        client.Send(emailMessage);
    }
}
