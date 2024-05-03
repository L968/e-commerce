using Ecommerce.Authorization.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Authorization.DependencyInjections.Extensions;

public static class AuthenticationExtensions
{
    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(auth =>
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

        services.AddAuthorizationBuilder()
            .AddPolicy("ReadMetrics", policy =>
            {
                policy.RequireRole("admin");
                policy.RequireAuthenticatedUser();
            });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultPhoneProvider;
        })
        .AddEntityFrameworkStores<AuthorizationContext>()
        .AddDefaultTokenProviders();
    }
}
