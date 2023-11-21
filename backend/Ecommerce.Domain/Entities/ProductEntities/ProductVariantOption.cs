namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductVariantOption
{
    public int Id { get; private set; }
    public int ProductVariationId { get; private set; }
    public int VariantOptionId { get; private set; }
}
