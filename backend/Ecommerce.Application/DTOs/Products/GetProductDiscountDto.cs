using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.DTOs.Products;

public class GetProductDiscountDto
{
    public int Id { get; init; }
    public Guid ProductId { get; init; }
    public string Name { get; init; } = "";
    public decimal DiscountValue { get; init; }
    public DiscountUnit DiscountUnit { get; init; }
    public decimal? MaximumDiscountAmount { get; init; }
    public DateTime ValidFrom { get; init; }
    public DateTime? ValidUntil { get; init; }
}
