namespace Ecommerce.Models.ProductModels
{
    public class ProductImage
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [Required]
        public string? ImagePath { get; set; }
    }
}