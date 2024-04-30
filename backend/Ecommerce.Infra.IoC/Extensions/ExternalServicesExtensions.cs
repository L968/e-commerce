using Ecommerce.Application.Interfaces;
using Ecommerce.Common.Infra.Handlers;
using Ecommerce.Infra.IoC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.Extensions;

public static class ExternalServicesExtensions
{
    public static void AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddHttpClient<IAuthorizationService, AuthorizationService>((serviceProvider, client) =>
        {
            client.Timeout = TimeSpan.FromSeconds(Config.AuthorizationServiceTimeout);
            client.BaseAddress = new Uri(Config.AuthorizationServiceBaseUrl);
        })
        .AddHttpMessageHandler(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            return new AuthorizationHeaderHandler(httpContextAccessor);
        });
    }
}
