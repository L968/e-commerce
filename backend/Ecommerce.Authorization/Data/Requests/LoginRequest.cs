namespace Ecommerce.Authorization.Data.Requests
{
    public class LoginRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}