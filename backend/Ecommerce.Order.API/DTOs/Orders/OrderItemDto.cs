namespace Ecommerce.Order.API.DTOs.Orders;

public record OrderItemDto
{
    public Guid ProductCombinationId { get; init; }
    public string ProductName { get; init; } = "";
    public string ProductSku { get; init; } = "";
    public string? ProductImagePath { get; init; }
    public string Description { get; init; } = "";
    public decimal TotalAmount { get; init; }
    public decimal ProductUnitPrice { get; init; }
    public decimal? ProductDiscount { get; init; }
    public int Quantity { get; init; }
}
