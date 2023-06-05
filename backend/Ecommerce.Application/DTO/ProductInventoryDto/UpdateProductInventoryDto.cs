namespace Ecommerce.Application.DTO.ProductInventoryDto;

public class UpdateProductInventoryDto
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public int? Quantity { get; init; }
}