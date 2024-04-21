namespace Ecommerce.Order.API.DTOs.OrderCheckout;

public record OrderCheckoutItemDto
{
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
}
