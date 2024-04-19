namespace Ecommerce.Application.DTOs.Products;

public record ReduceStockRequest
{
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
}
