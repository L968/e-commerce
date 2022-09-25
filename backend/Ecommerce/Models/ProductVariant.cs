namespace Ecommerce.Models
{
    public class ProductVariant
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Value { get; set; }

        [Required]
        public int? ParentProductId { get; set; }

        [Required]
        public int? ProductId { get; set; }
    }
}