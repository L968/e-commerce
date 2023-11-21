using Ecommerce.Domain.Entities.CartEntities;

namespace Ecommerce.Consumers.OrderCheckout;

public class OrderCheckout
{
    public int UserId { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
    public string ShippingPostalCode { get; set; } = "";
    public string ShippingStreetName { get; set; } = "";
    public string ShippingBuildingNumber { get; set; } = "";
    public string? ShippingComplement { get; set; }
    public string? ShippingNeighborhood { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingCountry { get; set; }
}
