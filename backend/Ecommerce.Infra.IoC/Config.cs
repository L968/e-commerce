using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.IoC;

public static class Config
{
    public static string JwtKey { get; private set; } = "";
    public static string[] AllowedOrigins { get; private set; } = [];
    public static string ConnectionString { get; private set; } = "";

    public static string AuthorizationServiceBaseUrl { get; private set; } = "";
    public static int AuthorizationServiceTimeout { get; private set; }

    public static string AzureStorageConnectionString { get; private set; } = "";
    public static string AzureStorageContainer { get; private set; } = "";

    public static void Init(IConfiguration configuration)
    {
        JwtKey = configuration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key");
        AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? throw new ArgumentNullException("AllowedOrigins");
        ConnectionString = configuration.GetConnectionString("Connection") ?? throw new ArgumentNullException("ConnectionStrings:Connection");

        AuthorizationServiceBaseUrl = configuration.GetValue<string>("AuthorizationService:BaseUrl") ?? throw new ArgumentNullException("AuthorizationService:BaseUrl");
        AuthorizationServiceTimeout = configuration.GetValue<int?>("AuthorizationService:Timeout") ?? throw new ArgumentNullException("AuthorizationService:Timeout");

        AzureStorageConnectionString = configuration.GetValue<string>("AzureStorage:ConnectionString") ?? throw new ArgumentNullException("AzureStorage:ConnectionString");
        AzureStorageContainer = configuration.GetValue<string>("AzureStorage:Container") ?? throw new ArgumentNullException("AzureStorage:Container");
    }
}
