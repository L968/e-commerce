namespace Ecommerce.Authorization.Data.DTO;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required]
    [Compare("Password")]
    public string RePassword { get; set; } = "";
}
