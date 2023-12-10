namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class ProductVariationOption
{
    public int Id { get; private set; }
    public Guid ProductVariationId { get; private set; }
    public int VariantOptionId { get; private set; }

    public ProductVariation? ProductVariation { get; private set; }
    public VariantOption? VariantOption { get; private set; }

    private ProductVariationOption() { }

    public ProductVariationOption(Guid productVariationId, int variantOptionId)
    {
        ProductVariationId = productVariationId;
        VariantOptionId = variantOptionId;
    }
}
