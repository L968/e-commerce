namespace Ecommerce.Application.DTO.ProductInventoryDto;

public record CreateProductInventoryDto
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public int? Quantity { get; init; }

    [Required]
    public int? ProductId { get; init; }
}