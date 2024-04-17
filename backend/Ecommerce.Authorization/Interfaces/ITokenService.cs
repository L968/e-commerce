namespace Ecommerce.Authorization.Interfaces;

public interface ITokenService
{
    Token CreateToken(CustomIdentityUser user, string role);
}
