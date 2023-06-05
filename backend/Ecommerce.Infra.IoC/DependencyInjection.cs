using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.AddressService;
using Ecommerce.Application.Interfaces.ProductServices;
using Ecommerce.Application.Mappings;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.AddressServices;
using Ecommerce.Application.Services.ProductServices;
using Ecommerce.Domain.Interfaces.AddressRepositories;
using Ecommerce.Domain.Interfaces.ProductRepositories;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Repositories.AddressRepositories;
using Ecommerce.Infra.Data.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseSnakeCaseNamingConvention()
                .UseMySql(
                    connectionString,
                    serverVersion,
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) // Indicando onde serão executadas as migrations
                )
        );

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        return services;
    }
}