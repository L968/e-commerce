namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class ProductCategoryVariant
{
    public Guid Id { get; private set; }
    public Guid ProductCategoryId { get; private set; }
    public Guid VariantId { get; private set; }

    public ProductCategory? ProductCategory { get; private set; }
    public Variant? Variant { get; private set; }

    public ProductCategoryVariant(Guid productCategoryId, Guid variantId)
    {
        Id = new Guid();
        ProductCategoryId = productCategoryId;
        VariantId = variantId;
    }
}
