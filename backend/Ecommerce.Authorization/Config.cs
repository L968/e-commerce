namespace Ecommerce.Authorization;

public class Config
{
    private readonly IConfiguration _configuration;

    public static string JwtKey { get; set; } = "";
    public static string AdminInfoPassword { get; set; } = "";

    public static string EmailSettingsFrom { get; set; } = "";
    public static string EmailSettingsPassword { get; set; } = "";
    public static string EmailSettingsSmtpServer { get; set; } = "";
    public static int EmailSettingsPort { get; set; }

    public static string TwilioSID { get; set; } = "";
    public static string TwilioAuthToken { get; set; } = "";
    public static string TwilioPhoneNumber { get; set; } = "";

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Init()
    {
        JwtKey = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
        AdminInfoPassword = _configuration.GetValue<string>("AdminInfo:Password") ?? throw new ArgumentNullException("AdminInfo:Password");

        EmailSettingsFrom = _configuration.GetValue<string>("EmailSettings:From") ?? throw new ArgumentNullException("EmailSettings:From");
        EmailSettingsPassword = _configuration.GetValue<string>("EmailSettings:Password") ?? throw new ArgumentNullException("EmailSettings:Password");
        EmailSettingsSmtpServer = _configuration.GetValue<string>("EmailSettings:SmtpServer") ?? throw new ArgumentNullException("EmailSettings:SmtpServer");
        EmailSettingsPort = _configuration.GetValue<int>("EmailSettings:Port");

        if (EmailSettingsPort <= 0) throw new ArgumentNullException("EmailSettings:Port");

        TwilioSID = _configuration.GetValue<string>("Twilio:SID") ?? throw new ArgumentNullException("Twilio:SID");
        TwilioAuthToken = _configuration.GetValue<string>("Twilio:AuthToken") ?? throw new ArgumentNullException("Twilio:AuthToken");
        TwilioPhoneNumber = _configuration.GetValue<string>("Twilio:PhoneNumber") ?? throw new ArgumentNullException("Twilio:PhoneNumber");
    }
}
