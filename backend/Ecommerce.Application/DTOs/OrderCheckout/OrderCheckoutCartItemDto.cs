namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutCartItemDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Guid ProductCombinationId { get; set; }
    public int Quantity { get; set; }
    public bool IsSelectedForCheckout { get; set; }
}
