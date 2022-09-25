namespace Ecommerce.Models
{
    public class Payment : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? OrderId { get; set; }

        [Required]
        public decimal? Value { get; set; }

        [Required]
        public string? Currency { get; set; }

        [Required]
        public string? Method { get; set; }

        [Required]
        public string? Status { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}