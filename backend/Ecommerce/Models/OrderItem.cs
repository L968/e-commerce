namespace Ecommerce.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? OrderId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductSku { get; set; }

        [Required]
        public string? ProductImagePath { get; set; }

        public decimal? ProductDiscount { get; set; }

        [Required]
        public decimal? ProductPrice { get; set; }
    }
}