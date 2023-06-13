using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Ecommerce.Application.IntegrationTests;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == GetCurrentUserId()));

            var connectionString = builder.Configuration.GetConnectionString("TestsConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services
                .Remove<DbContextOptions<AppDbContext>>()
                .AddDbContext<AppDbContext>((sp, options) =>
                    options
                        .UseSnakeCaseNamingConvention()
                        .UseMySql(
                            connectionString,
                            serverVersion,
                            builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                        );
        });
    }
}
