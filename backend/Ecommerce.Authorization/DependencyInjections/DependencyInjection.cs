using Ecommerce.Authorization.DependencyInjections.Extensions;

namespace Ecommerce.Authorization.DependencyInjections;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Config.Init(configuration);

        services.ConfigureDatabase();
        services.AddServices(typeof(Program).Assembly.GetName().Name!);
        services.ConfigureVersioning();
        services.ConfigureAuthentication();
        services.ConfigureIdentity();
        services.ConfigureCorsPolicy();

        return services;
    }
}
