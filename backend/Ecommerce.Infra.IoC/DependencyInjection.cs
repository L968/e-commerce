using Ecommerce.Application.Common.Behaviours;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.ProductServices;
using Ecommerce.Application.Mappings;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.ProductServices;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.Repositories.CartRepositories;
using Ecommerce.Domain.Repositories.ProductRepositories;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Repositories;
using Ecommerce.Infra.Data.Repositories.CartRepositories;
using Ecommerce.Infra.Data.Repositories.ProductRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        var applicationAssembly = AppDomain.CurrentDomain.Load("Ecommerce.Application");

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(applicationAssembly);
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddFluentValidationAutoValidation();
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");

        return services;
    }
}