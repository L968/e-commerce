using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Ecommerce.Authorization.Services
{
    public class SmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendPhoneNumberConfirmationSms(string to, string confirmationToken)
        {
            Send(to, $"Confirm your phone number: {confirmationToken}");
        }

        public void SendTwoFactorTokenSms(string to, string twoFactorToken)
        {
            Send(to, $"Two factor authentication token: {twoFactorToken}");
        }

        private void Send(string to, string body)
        {
            var username = _configuration.GetValue<string>("Twilio:SID");
            var password = _configuration.GetValue<string>("Twilio:AuthToken");
            var twilioPhoneNumber = _configuration.GetValue<string>("Twilio:PhoneNumber");
            TwilioClient.Init(username, password);

            MessageResource.Create(
                from: twilioPhoneNumber,
                to: new PhoneNumber(to),
                body: body
            );
        }
    }
}