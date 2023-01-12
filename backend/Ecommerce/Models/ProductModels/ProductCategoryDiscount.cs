namespace Ecommerce.Models.ProductModels
{
    public class ProductCategoryDiscount : BaseModel
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
        public int? ProductCategoryId { get; set; }

        [JsonIgnore]
        public ProductCategory? ProductCategory { get; set; }
    }
}