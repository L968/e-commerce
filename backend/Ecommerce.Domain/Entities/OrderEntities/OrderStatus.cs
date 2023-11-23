namespace Ecommerce.Domain.Entities.OrderEntities;

public enum OrderStatus
{
    PendingPayment,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Refunded,
    Returned
}
