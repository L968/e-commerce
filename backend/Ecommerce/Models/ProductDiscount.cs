namespace Ecommerce.Models
{
    public class ProductDiscount : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal? DiscountValue { get; set; }

        [Required]
        public string? DiscountUnit { get; set; }

        public decimal? MaximumDiscountAmount { get; set; }

        [Required]
        public DateTime? ValidFrom { get; set; }

        [Required]
        public DateTime? ValidUntil { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}