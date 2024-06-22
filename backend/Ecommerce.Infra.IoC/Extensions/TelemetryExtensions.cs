using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System.Reflection;

namespace Ecommerce.Infra.IoC.Extensions;

internal static class TelemetryExtensions
{
    public static void AddTelemetry(this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry(options =>
        {
            options.ConnectionString = Config.ApplicationInsightsConnectionString;
        });

        services.AddSingleton<TelemetryClient>();
    }

    public static void AddLog(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddApplicationInsights(config =>
                    config.ConnectionString = Config.ApplicationInsightsConnectionString,
                    configureApplicationInsightsLoggerOptions: (options) => { }
            );

            string? assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>(assemblyName, LogLevel.Trace);
        });
    }
}
