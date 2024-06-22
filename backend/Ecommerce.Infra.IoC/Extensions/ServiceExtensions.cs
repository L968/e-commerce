using Ecommerce.Application.Common.Behaviours;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Common.Infra.Handlers;
using Ecommerce.Infra.IoC.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Ecommerce.Infra.IoC.Extensions;

internal static class ServiceExtensions
{
    public static void AddApplicationLibraries(this IServiceCollection services, string assemblyName)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        var applicationAssembly = AppDomain.CurrentDomain.Load(assemblyName);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(applicationAssembly);
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddFluentValidationAutoValidation();
        ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;

        services.AddAutoMapper(applicationAssembly);
    }

    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<AuthorizationHeaderHandler>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}
