namespace Ecommerce.Application.DTOs.Products;

public record GetProductInventoryDto
{
    public int Id { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Stock { get; private set; }
}
