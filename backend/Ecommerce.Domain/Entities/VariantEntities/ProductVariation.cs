namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class ProductVariation
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int VariantId { get; private set; }

    public Product? Product { get; private set; }
    public Variant? Variant { get; private set; }

    private readonly List<ProductVariationOption> _variationOptions = new();
    public IReadOnlyCollection<ProductVariationOption> VariationOptions => _variationOptions;

    public ProductVariation(Guid productId, int variantId)
    {
        Id = new Guid();
        ProductId = productId;
        VariantId = variantId;
    }

    public void AddVariantOption(int variantOptionId)
    {
        _variationOptions.Add(new ProductVariationOption(Id, variantOptionId));
    }
}
