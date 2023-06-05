namespace Ecommerce.Authorization.Data.Requests;

public class LoginRequest
{
    [Required]
    public string EmailOrPhoneNumber { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}