using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application;

public class Config(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public static string AzureStorageConnectionString { get; set; } = "";
    public static string AzureStorageContainer { get; set; } = "";

    public void Init()
    {
        AzureStorageConnectionString = _configuration.GetValue<string>("AzureStorage:ConnectionString") ?? throw new ArgumentNullException("AzureStorage:ConnectionString");
        AzureStorageContainer = _configuration.GetValue<string>("AzureStorage:Container") ?? throw new ArgumentNullException("AzureStorage:Container");
    }
}
