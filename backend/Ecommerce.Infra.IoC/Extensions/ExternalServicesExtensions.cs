using Ecommerce.Application.Common.Handlers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC.Extensions;

public static class ExternalServicesExtensions
{
    public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBlobStorageService, BlobStorageService>();
        services.AddHttpClient<IAuthorizationService, AuthorizationService>((serviceProvider, client) =>
        {
            string? timeout = configuration["AuthorizationService:Timeout"];

            if (string.IsNullOrEmpty(timeout))
                throw new InvalidOperationException("AuthorizationService:Timeout configuration is missing or empty");

            if (!int.TryParse(timeout, out int timeoutSeconds))
                throw new InvalidOperationException("AuthorizationService:Timeout configuration is not a valid integer");

            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            client.BaseAddress = new Uri(configuration["AuthorizationService:BaseUrl"] ?? throw new InvalidOperationException("AuthorizationService:BaseUrl configuration is missing or empty"));
        })
        .AddHttpMessageHandler(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            return new AuthorizationHeaderHandler(httpContextAccessor);
        });
    }
}
