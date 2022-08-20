namespace Ecommerce.Data.DTO
{
    public class UpdateProductInventoryDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public int? Quantity { get; init; }
    }
}