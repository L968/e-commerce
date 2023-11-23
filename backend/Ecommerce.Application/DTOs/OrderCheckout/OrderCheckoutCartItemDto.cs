namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutCartItemDto
{
    public int CartId { get; init; }
    public Guid ProductCombinationId { get; init; }
    public int Quantity { get; init; }
}
