namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutItemDto
{
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
}
