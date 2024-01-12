using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class OrderHistory
{
    public int Id { get; private set; }
    public Guid OrderId { get; private set; }
    public OrderStatus Status { get; private set; }
    public string? Notes { get; private set; }
    public DateTime Date { get; private set; }

    public Order? Order { get; private set; }

    private OrderHistory() { }

    public OrderHistory(Guid orderId, OrderStatus status, string? notes = null)
    {
        OrderId = orderId;
        Status = status;
        Notes = notes;
        Date = DateTime.Now;
    }
}
