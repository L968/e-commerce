using Ecommerce.Common.Infra.Binders;
using Ecommerce.Common.Infra.Handlers;
using Ecommerce.Order.API;
using Ecommerce.Order.API.Context;
using Ecommerce.Order.API.Handlers;
using Ecommerce.Order.API.Interfaces;
using Ecommerce.Order.API.Mappings;
using Ecommerce.Order.API.Repositories;
using Ecommerce.Order.API.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Config.Init(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpClient<IEcommerceService, EcommerceService>((serviceProvider, client) =>
{
    client.Timeout = TimeSpan.FromSeconds(Config.EcommerceServiceTimeout);
    client.BaseAddress = new Uri(Config.EcommerceServiceBaseUrl);
})
.AddHttpMessageHandler(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new AuthorizationHeaderHandler(httpContextAccessor);
});

builder.Services.AddHttpClient<IPayPalService, PayPalService>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri(Config.PayPalBaseAddress);
});

var connectionString = builder.Configuration.GetConnectionString("Connection");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseSnakeCaseNamingConvention()
        .UseMySql(
            connectionString,
            serverVersion,
            mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
));

builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

builder.Services.AddMvcCore(options =>
{
    options.ModelBinderProviders.Insert(0, new GridParamsBinderProvider());
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(token =>
{
    token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.JwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
        {
            corsBuilder.WithOrigins(Config.AllowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseMiddleware<GridParamsExceptionHandler>();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
