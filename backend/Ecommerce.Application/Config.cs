using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application;

public class Config
{
    private readonly IConfiguration _configuration;

    public static string AzureStorageConnectionString { get; set; } = "";
    public static string AzureStorageContainer { get; set; } = "";

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Init()
    {
        AzureStorageConnectionString = _configuration.GetValue<string>("AzureStorage:ConnectionString") ?? throw new ArgumentNullException("AzureStorage:ConnectionString");
        AzureStorageContainer = _configuration.GetValue<string>("AzureStorage:Container") ?? throw new ArgumentNullException("AzureStorage:Container");
    }
}
