namespace Ecommerce.Application.DTOs;

public record OrderDto
{
    public Guid Id { get; init; }
    public string Status { get; init; } = "";
    public string PaymentMethod { get; set; } = "";
    public decimal Subtotal { get; init; }
    public decimal ShippingCost { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal? TotalDiscount { get; init; }
    public string ShippingAddress { get; init; } = "";
    public DateTime CreatedAt { get; init; }

    public List<OrderItemDto> Items { get; init; } = [];
    public List<OrderHistoryDto> History { get; init; } = [];
}
