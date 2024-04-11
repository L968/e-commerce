using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.DTOs.OrderCheckout;

public record OrderCheckoutDto
{
    public int UserId { get; set; }
    public List<OrderCheckoutItemDto> OrderCheckoutItems { get; init; } = null!;
    public Guid ShippingAddressId { get; init; }
    public string? ExternalPaymentId { get; set; }
    public PaymentMethod PaymentMethod { get; init; }
}
