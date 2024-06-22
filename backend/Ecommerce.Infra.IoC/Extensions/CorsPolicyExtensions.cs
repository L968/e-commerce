using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.Extensions;

internal static class CorsPolicyExtensions
{
    public static void AddCorsPolicy(this IServiceCollection services)
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
