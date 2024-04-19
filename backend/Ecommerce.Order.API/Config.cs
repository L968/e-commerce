namespace Ecommerce.Order.API;

public static class Config
{
    public static string JwtKey { get; private set; } = "";
    public static string[] AllowedOrigins { get; private set; } = [];

    public static string EcommerceServiceBaseUrl { get; private set; } = "";
    public static int EcommerceServiceTimeout { get; private set; }

    public static string PayPalBaseAddress{ get; private set; } = "";
    public static string PayPalReturnUrl { get; private set; } = "";
    public static string PayPalCancelUrl { get; private set; } = "";
    public static string PayPalClientId { get; private set; } = "";
    public static string PayPalClientSecret { get; private set; } = "";

    public static void Init(IConfiguration configuration)
    {
        JwtKey = configuration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key");
        AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ArgumentNullException("AllowedOrigins");

        EcommerceServiceBaseUrl = configuration.GetValue<string>("EcommerceService:BaseUrl") ?? throw new ArgumentNullException("EcommerceService:BaseUrl");
        EcommerceServiceTimeout = configuration.GetValue<int?>("EcommerceService:Timeout") ?? throw new ArgumentNullException("EcommerceService:Timeout");

        PayPalBaseAddress = configuration.GetValue<string>("PayPal:BaseAddress") ?? throw new ArgumentNullException("PayPal:BaseAddress");
        PayPalReturnUrl = configuration.GetValue<string>("PayPal:ReturnUrl") ?? throw new ArgumentNullException("PayPal:ReturnUrl");
        PayPalCancelUrl = configuration.GetValue<string>("PayPal:CancelUrl") ?? throw new ArgumentNullException("PayPal:CancelUrl");
        PayPalClientId = configuration.GetValue<string>("PayPal:ClientId") ?? throw new ArgumentNullException("PayPal:ClientId");
        PayPalClientSecret = configuration.GetValue<string>("PayPal:ClientSecret") ?? throw new ArgumentNullException("PayPal:ClientSecret");
    }
}
