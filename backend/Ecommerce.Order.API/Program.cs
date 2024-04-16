using Ecommerce.Application.Common.Handlers;
using Ecommerce.Order.API;
using Ecommerce.Order.API.Context;
using Ecommerce.Order.API.Mappings;
using Ecommerce.Order.API.Repositories;
using Ecommerce.Order.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

new Config(builder.Configuration).Init();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpClient<IEcommerceService, EcommerceService>((serviceProvider, client) =>
{
    string? timeout = builder.Configuration["EcommerceService:Timeout"];

    if (string.IsNullOrEmpty(timeout))
        throw new InvalidOperationException("EcommerceService:Timeout configuration is missing or empty");

    if (!int.TryParse(timeout, out int timeoutSeconds))
        throw new InvalidOperationException("EcommerceService:Timeout configuration is not a valid integer");

    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
    client.BaseAddress = new Uri(builder.Configuration["EcommerceService:BaseUrl"] ?? throw new InvalidOperationException("EcommerceService:BaseUrl configuration is missing or empty"));
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

builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseSnakeCaseNamingConvention()
        .UseMySql(
            connectionString,
            serverVersion,
            mysqlOptions => mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        )
);

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:3000")
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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
