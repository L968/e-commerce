using Ecommerce.Authorization.Data;

namespace Ecommerce.Authorization.DependencyInjections.Extensions;

public static partial class DatabaseExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        var serverVersion = ServerVersion.AutoDetect(Config.ConnectionString);

        services.AddDbContext<AuthorizationContext>(options =>
            options
                .UseSnakeCaseNamingConvention()
                .UseMySql(
                    Config.ConnectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AuthorizationContext).Assembly.FullName)
                )
        );
    }
}
