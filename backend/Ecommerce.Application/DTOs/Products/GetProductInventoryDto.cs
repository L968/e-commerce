namespace Ecommerce.Application.DTOs.Products;

public record GetProductInventoryDto
{
    public int Id { get; init; }
    public Guid ProductCombinationId { get; init; }
    public int Stock { get; init; }
}
