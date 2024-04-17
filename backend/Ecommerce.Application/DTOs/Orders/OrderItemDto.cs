namespace Ecommerce.Application.DTOs;

public record OrderItemDto
{
    public Guid ProductCombinationId { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductSku { get; set; } = "";
    public string? ProductImagePath { get; set; }
    public string Description { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public decimal ProductUnitPrice { get; set; }
    public decimal? ProductDiscount { get; set; }
    public int Quantity { get; set; }
}
