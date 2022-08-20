namespace Authorization.Data.Requests
{
    public class RequestPasswordResetRequest
    {
        [Required]
        public string? Email { get; set; }
    }
}