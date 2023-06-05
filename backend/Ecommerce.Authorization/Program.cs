using System.Text;
using Ecommerce.Authorization;
using Ecommerce.Authorization.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

new Config(builder.Configuration).Init();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultPhoneProvider;
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

var connectionString = builder.Configuration.GetConnectionString("Connection");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<Context>(options =>
    options
        .UseSnakeCaseNamingConvention()
        .UseMySql(connectionString, serverVersion)
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.JwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<EmailService, EmailService>();
builder.Services.AddScoped<SmsService, SmsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();