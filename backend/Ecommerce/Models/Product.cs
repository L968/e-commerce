namespace Ecommerce.Models
{
    public class Product : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public Guid? Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Sku { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public decimal? Price { get; set; }

        [Required]
        public bool? Active { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public float? Weight { get; set; }

        [Required]
        public int? ProductCategoryId { get; set; }

        [JsonIgnore]
        public ProductCategory? ProductCategory { get; set; }

        [JsonIgnore]
        public ProductInventory? ProductInventory { get; set; }

        [JsonIgnore]
        public List<ProductImage>? Images { get; set; }

        public GetProductDto ToDto()
        {
            return new GetProductDto()
            {
                Id = Id,
                Guid = Guid,
                Name = Name,
                Description = Description,
                Sku = Sku,
                Weight = Weight,
                ProductCategoryId = ProductCategoryId,
                ProductCategory = ProductCategory,
                Images = Images,
                Price = Price,
            };
        }
    }
}