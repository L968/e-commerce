namespace Ecommerce.Authorization;

public static class Config
{
    public static string JwtKey { get; private set; } = "";
    public static string[] AllowedOrigins { get; private set; } = [];
    public static string ConnectionString { get; private set; } = "";
    public static string AdminInfoPassword { get; private set; } = "";

    public static string EmailSettingsFrom { get; private set; } = "";
    public static string EmailSettingsPassword { get; private set; } = "";
    public static string EmailSettingsSmtpServer { get; private set; } = "";
    public static int EmailSettingsPort { get; private set; }

    public static string TwilioSID { get; private set; } = "";
    public static string TwilioAuthToken { get; private set; } = "";
    public static string TwilioPhoneNumber { get; private set; } = "";

    public static void Init(IConfiguration configuration)
    {
        JwtKey = configuration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key");
        AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ArgumentNullException("AllowedOrigins");
        ConnectionString = configuration.GetConnectionString("AuthorizationConnection") ?? throw new ArgumentNullException("ConnectionStrings:AuthorizationConnection");
        AdminInfoPassword = configuration.GetValue<string>("AdminInfo:Password") ?? throw new ArgumentNullException("AdminInfo:Password");

        EmailSettingsFrom = configuration.GetValue<string>("EmailSettings:From") ?? throw new ArgumentNullException("EmailSettings:From");
        EmailSettingsPassword = configuration.GetValue<string>("EmailSettings:Password") ?? throw new ArgumentNullException("EmailSettings:Password");
        EmailSettingsSmtpServer = configuration.GetValue<string>("EmailSettings:SmtpServer") ?? throw new ArgumentNullException("EmailSettings:SmtpServer");
        EmailSettingsPort = configuration.GetValue<int>("EmailSettings:Port");

        if (EmailSettingsPort <= 0) throw new ArgumentNullException("EmailSettings:Port");

        TwilioSID = configuration.GetValue<string>("Twilio:SID") ?? throw new ArgumentNullException("Twilio:SID");
        TwilioAuthToken = configuration.GetValue<string>("Twilio:AuthToken") ?? throw new ArgumentNullException("Twilio:AuthToken");
        TwilioPhoneNumber = configuration.GetValue<string>("Twilio:PhoneNumber") ?? throw new ArgumentNullException("Twilio:PhoneNumber");
    }
}
