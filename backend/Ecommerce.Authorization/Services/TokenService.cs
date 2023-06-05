using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Authorization.Services;

public class TokenService
{
    public Token CreateToken(CustomIdentityUser user, string role)
    {
        var claims = new Claim[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.Role, role),
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