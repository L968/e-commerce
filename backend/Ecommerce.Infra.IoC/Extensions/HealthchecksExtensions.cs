using Ecommerce.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.Extensions;

internal static class HealthchecksExtensions
{
    public static void ConfigureHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>()
            .AddAzureBlobStorage(Config.AzureStorageConnectionString, containerName: Config.AzureStorageContainer);
    }
}
