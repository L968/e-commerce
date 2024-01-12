using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Order.API.DTOs;

public record OrderDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal? Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = "";

    public List<OrderHistory> History = new();
    public List<OrderItem> Items = new();
}
