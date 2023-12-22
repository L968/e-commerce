namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class VariantOption
{
    public int Id { get; private set; }
    public Guid VariantId { get; private set; }
    public string Name { get; private set; }

    public Variant? Variant { get; private set; }
    public List<ProductVariantOption>? ProductVariantOptions { get; private set; }

    private VariantOption() { }

    public VariantOption(Guid variantId, string name)
    {
        VariantId = variantId;
        Name = name;
    }
}
