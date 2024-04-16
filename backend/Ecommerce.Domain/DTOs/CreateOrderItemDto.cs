namespace Ecommerce.Domain.DTOs;

public record CreateOrderItemDto
{
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
    public string ProductName { get; init; }
    public string ProductSku { get; init; }
    public string? ProductImagePath { get; init; }
    public decimal ProductUnitPrice { get; init; }
    public decimal? ProductDiscount { get; init; }
}
