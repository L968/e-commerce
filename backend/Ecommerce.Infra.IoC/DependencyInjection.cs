using Ecommerce.Application;
using Ecommerce.Application.Common.Behaviours;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappings;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.Repositories.CartRepositories;
using Ecommerce.Domain.Repositories.ProductRepositories;
using Ecommerce.Domain.Repositories.VariantRepositories;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Repositories;
using Ecommerce.Infra.Data.Repositories.CartRepositories;
using Ecommerce.Infra.Data.Repositories.ProductRepositories;
using Ecommerce.Infra.Data.Repositories.VariantRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

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

        new Config(configuration).Init();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCombinationRepository, ProductCombinationRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
        services.AddScoped<IVariantOptionRepository, VariantOptionRepository>();

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
