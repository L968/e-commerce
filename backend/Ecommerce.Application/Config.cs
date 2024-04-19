using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application;

public static class Config
{
    public static string AzureStorageConnectionString { get; set; } = "";
    public static string AzureStorageContainer { get; set; } = "";

    public static void Init(IConfiguration configuration)
    {
        AzureStorageConnectionString = configuration.GetValue<string>("AzureStorage:ConnectionString") ?? throw new ArgumentNullException("AzureStorage:ConnectionString");
        AzureStorageContainer = configuration.GetValue<string>("AzureStorage:Container") ?? throw new ArgumentNullException("AzureStorage:Container");
    }
}
