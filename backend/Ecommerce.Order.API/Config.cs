namespace Ecommerce.Order.API;

public class Config(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public static string PayPalBaseAddress{ get; set; } = "";
    public static string PayPalReturnUrl { get; set; } = "";
    public static string PayPalCancelUrl { get; set; } = "";
    public static string PayPalClientId { get; set; } = "";
    public static string PayPalClientSecret { get; set; } = "";

    public void Init()
    {
        PayPalBaseAddress = _configuration.GetValue<string>("PayPal:BaseAddress") ?? throw new ArgumentNullException("PayPal:BaseAddress");
        PayPalReturnUrl = _configuration.GetValue<string>("PayPal:ReturnUrl") ?? throw new ArgumentNullException("PayPal:ReturnUrl");
        PayPalCancelUrl = _configuration.GetValue<string>("PayPal:CancelUrl") ?? throw new ArgumentNullException("PayPal:CancelUrl");
        PayPalClientId = _configuration.GetValue<string>("PayPal:ClientId") ?? throw new ArgumentNullException("PayPal:ClientId");
        PayPalClientSecret = _configuration.GetValue<string>("PayPal:ClientSecret") ?? throw new ArgumentNullException("PayPal:ClientSecret");
    }
}
