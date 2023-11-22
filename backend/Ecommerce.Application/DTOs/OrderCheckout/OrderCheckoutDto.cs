namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutDto
{
    public int UserId { get; init; }
    public List<OrderCheckoutCartItemDto> CartItems { get; init; } = null!;
    public string ShippingPostalCode { get; set; } = "";
    public string ShippingStreetName { get; set; } = "";
    public string ShippingBuildingNumber { get; set; } = "";
    public string? ShippingComplement { get; set; }
    public string? ShippingNeighborhood { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingCountry { get; set; }
}
