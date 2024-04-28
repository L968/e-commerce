using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.Extensions;

public static class CorsPolicyExtensions
{
    public static void ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(Config.AllowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
            });
        });
    }
}
