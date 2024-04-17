using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Authorization.Services;

public class TokenService : ITokenService
{
    public Token CreateToken(CustomIdentityUser user, string role)
    {
        var claims = new Claim[]
        {
            new("id", user.Id.ToString()),
            new("username", user.UserName),
            new(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.JwtKey));
        var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credencials,
            expires: DateTime.UtcNow.AddDays(1)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token(tokenString);
    }
}
