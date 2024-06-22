using Ecommerce.Infra.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Config.Init(configuration);

        services.AddLog();
        services.AddTelemetry();
        services.AddDatabase();
        services.AddRepositories("Ecommerce.Infra.Data");
        services.AddExternalServices();
        services.AddApplicationLibraries("Ecommerce.Application");
        services.ConfigureApplication();
        services.AddCorsPolicy();
        services.ConfigureAuthentication();
        services.ConfigureHealthChecks();

        return services;
    }
}
