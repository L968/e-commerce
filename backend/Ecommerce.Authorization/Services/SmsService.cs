using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Ecommerce.Authorization.Services;

public class SmsService : ISmsService
{
    public async Task SendPhoneNumberConfirmationSms(string to, string confirmationToken)
    {
        await Send(to, $"Confirm your phone number: {confirmationToken}");
    }

    public async Task SendTwoFactorTokenSms(string to, string twoFactorToken)
    {
        await Send(to, $"Two factor authentication token: {twoFactorToken}");
    }

    private static async Task Send(string to, string body)
    {
        var username = Config.TwilioSID;
        var password = Config.TwilioAuthToken;
        var twilioPhoneNumber = Config.TwilioPhoneNumber;

        TwilioClient.Init(username, password);

        await MessageResource.CreateAsync(
            from: twilioPhoneNumber,
            to: new PhoneNumber(to),
            body: body
        );
    }
}
