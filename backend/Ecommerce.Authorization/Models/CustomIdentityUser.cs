namespace Ecommerce.Authorization.Models;

public class CustomIdentityUser : IdentityUser<int>
{
    public string Name { get; set; } = "";
    public Guid? DefaultAddressId { get; set; }
}
