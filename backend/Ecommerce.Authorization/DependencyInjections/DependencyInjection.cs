using Ecommerce.Authorization.DependencyInjections.Extensions;

namespace Ecommerce.Authorization.DependencyInjections;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Config.Init(configuration);

        services.ConfigureDatabase();
        services.ConfigureIdentity();
        services.ConfigureAuthentication();
        services.ConfigureCorsPolicy();
        services.ConfigureVersioning();
        services.AddServices(typeof(Program).Assembly.GetName().Name!);

        return services;
    }
}
