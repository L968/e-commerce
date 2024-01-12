namespace Ecommerce.Authorization.Models;

public class CustomIdentityUser : IdentityUser<int>
{
    public string Name { get; set; } = "";
    public int? DefaultAddressId { get; set; }
}
