namespace Ecommerce.Models.ProductModels
{
    public class ProductCategory : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public Guid? Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Product>? Products { get; set; }

        public GetProductCategoryDto ToDto()
        {
            return new GetProductCategoryDto()
            {
                Id = Id,
                Guid = Guid,
                Name = Name,
                Description = Description,
                Products = Products,
            };
        }
    }
}