namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutCartItemDto
{
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
}
