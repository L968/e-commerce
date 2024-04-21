namespace Ecommerce.Order.API.DTOs.Orders;

public record OrderStatusCountDto
{
    public int PendingPaymentCount { get; init; }
    public int ProcessingCount { get; init; }
    public int ShippedCount { get; init; }
}
