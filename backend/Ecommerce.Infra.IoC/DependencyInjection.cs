using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.ProductServices;
using Ecommerce.Application.Mappings;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.ProductServices;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.ProductRepositories;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Repositories.AddressRepositories;
using Ecommerce.Infra.Data.Repositories.ProductRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
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
                    mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
        );

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        var applicationAssembly = AppDomain.CurrentDomain.Load("Ecommerce.Application");

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddFluentValidationAutoValidation();
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");

        return services;
    }
}