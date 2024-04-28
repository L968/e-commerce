using Ecommerce.Application;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Config.Init(configuration);

        services.ConfigureDatabase(configuration);
        services.AddRepositories("Ecommerce.Infra.Data");
        services.AddExternalServices(configuration);
        services.ConfigureLibraries("Ecommerce.Application");
        services.ConfigureApplication();
        services.ConfigureCorsPolicy();
        services.ConfigureAuthorization(configuration);

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>()
            .AddAzureBlobStorage(Config.AzureStorageConnectionString, containerName: Config.AzureStorageContainer);

        return services;
    }
}
