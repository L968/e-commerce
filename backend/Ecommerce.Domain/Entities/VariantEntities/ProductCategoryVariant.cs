namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class ProductCategoryVariant
{
    public Guid Id { get; private set; }
    public Guid ProductCategoryId { get; private set; }
    public int VariantId { get; private set; }

    public ProductCategory? ProductCategory { get; private set; }
    public Variant? Variant { get; private set; }

    public ProductCategoryVariant(Guid productCategoryId, int variantId)
    {
        Id = new Guid();
        ProductCategoryId = productCategoryId;
        VariantId = variantId;
    }
}
