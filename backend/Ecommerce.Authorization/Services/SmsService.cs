using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Ecommerce.Authorization.Services;

public class SmsService
{
    public void SendPhoneNumberConfirmationSms(string to, string confirmationToken)
    {
        Send(to, $"Confirm your phone number: {confirmationToken}");
    }

    public void SendTwoFactorTokenSms(string to, string twoFactorToken)
    {
        Send(to, $"Two factor authentication token: {twoFactorToken}");
    }

    private static void Send(string to, string body)
    {
        var username = Config.TwilioSID;
        var password = Config.TwilioAuthToken;
        var twilioPhoneNumber = Config.TwilioPhoneNumber;

        TwilioClient.Init(username, password);

        MessageResource.Create(
            from: twilioPhoneNumber,
            to: new PhoneNumber(to),
            body: body
        );
    }
}
