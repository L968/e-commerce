using MimeKit;
using MailKit.Net.Smtp;

namespace Ecommerce.Authorization.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailConfirmationEmail(string to, int userId, string confirmationToken)
        {
            var mailboxAdressess = new List<MailboxAddress> { new MailboxAddress("", to) };

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(mailboxAdressess);
            emailMessage.Subject = "Verify your new Ecommerce account";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = $"To verify your email address, please use the following link: \n\nhttps://localhost:7283/user/activate?id={userId}&confirmationCode={confirmationToken}"
            };

            Send(emailMessage);
        }

        public void SendResetPasswordEmail(string to, string passwordResetToken)
        {
            var mailboxAdressess = new List<MailboxAddress> { new MailboxAddress("", to) };

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(mailboxAdressess);
            emailMessage.Subject = "Reset your Ecommerce password";

            string htmlBody = Properties.Resources.reset_password_email.Replace("{resetPasswordUrl}", $"https://localhost:7283/reset-password/{passwordResetToken}");
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody;

            emailMessage.Body = bodyBuilder.ToMessageBody();

            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();

            client.Connect(
                _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                _configuration.GetValue<int>("EmailSettings:Port"),
                true
            );
            client.AuthenticationMechanisms.Remove("XOUATH2");
            client.Authenticate(
                _configuration.GetValue<string>("EmailSettings:From"),
                _configuration.GetValue<string>("EmailSettings:Password")
            );

            client.Send(emailMessage);
        }
    }
}