using System.Reflection;

namespace Ecommerce.Authorization.DependencyInjections.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, params string[] assemblyNames)
    {
        var assemblies = assemblyNames.Select(Assembly.Load).ToArray();
        var repositoryTypes = assemblies.SelectMany(assembly =>
            assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")));

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
