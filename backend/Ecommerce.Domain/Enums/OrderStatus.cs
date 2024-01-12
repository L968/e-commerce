namespace Ecommerce.Domain.Enums;

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
