namespace Ecommerce.Authorization.Data.Requests
{
    public class ActivateUserRequest
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string? ConfirmationCode { get; set; }
    }
}