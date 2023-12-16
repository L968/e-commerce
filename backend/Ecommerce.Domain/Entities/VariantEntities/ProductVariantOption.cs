namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class ProductVariantOption
{
    public int Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int VariantOptionId { get; private set; }

    public Product? Product { get; private set; }
    public VariantOption? VariantOption { get; private set; }

    private ProductVariantOption() { }

    public ProductVariantOption(Guid productId, int variantOptionId)
    {
        ProductId = productId;
        VariantOptionId = variantOptionId;
    }
}
