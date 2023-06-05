namespace Ecommerce.Application.DTO.ProductInventoryDto;

public class GetProductInventoryDto
{
    [Key]
    public int? Id { get; init; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public int? Quantity { get; init; }

    [Required]
    public int? ProductId { get; set; }

    public Product? Product { get; init; }
}