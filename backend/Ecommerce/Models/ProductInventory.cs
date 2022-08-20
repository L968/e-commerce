namespace Ecommerce.Models
{
    public class ProductInventory : BaseModel
    {
        [Key]
        [JsonIgnore]
        public int? Id { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public int? Quantity { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        public GetProductInventoryDto ToDto()
        {
            return new GetProductInventoryDto()
            {
                Id = Id,
                Quantity = Quantity,
                ProductId = ProductId,
                Product = Product,
            };
        }
    }
}