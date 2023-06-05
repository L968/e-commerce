namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductDiscount : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal DiscountValue { get; private set; }
    public string DiscountUnit { get; private set; }
    public decimal? MaximumDiscountAmount { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime? ValidUntil { get; private set; }
    public int ProductId { get; private set; }

    public Product? Product { get; set; }
}