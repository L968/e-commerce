using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Features.ProductDiscounts.Queries;

public class GetProductDiscountDto
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public DiscountUnit DiscountUnit { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
}