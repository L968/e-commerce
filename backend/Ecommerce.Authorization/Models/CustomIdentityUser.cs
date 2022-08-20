namespace Authorization.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string? Name { get; set; }
    }
}