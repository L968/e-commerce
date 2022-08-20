namespace Authorization.Data.Requests
{
    public class PasswordResetRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? RePassword { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Token { get; set; }
    }
}