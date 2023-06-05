namespace Ecommerce.Authorization.Data.Requests;

public class TwoFactorLoginRequest
{
    [Required]
    public int? UserId { get; set; }

    [Required]
    public string? TwoFactorToken { get; set; }
}