using Ecommerce.Domain.Repositories;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Infra.IoC.Extensions;

public static partial class DatabaseExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        var serverVersion = ServerVersion.AutoDetect(Config.ConnectionString);

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseSnakeCaseNamingConvention()
                .UseMySql(
                    Config.ConnectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddRepositories(this IServiceCollection services, params string[] assemblyNames)
    {
        var assemblies = assemblyNames.Select(Assembly.Load).ToArray();
        var repositoryTypes = assemblies.SelectMany(assembly =>
            assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")));

        foreach (var repository in repositoryTypes)
        {
            var interfaces = repository.GetInterfaces()
                .Where(i => i.Name.StartsWith("I") && i.Name.EndsWith(repository.Name))
                .ToList();

            foreach (var @interface in interfaces)
            {
                services.AddScoped(@interface, repository);
            }
        }
    }
}
