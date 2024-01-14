using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.DTOs;

public record OrderDto
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "";
    public decimal ShippingCost { get; set; }
    public decimal? Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = "";
    public DateTime CreatedAt { get; set; }

    public List<OrderHistoryDto> History { get; set; } = [];
    public List<OrderItemDto> Items { get; set; } = [];
}
