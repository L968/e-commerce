namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class VariantOption
{
    public int Id { get; private set; }
    public int VariantId { get; private set; }
    public string Name { get; private set; }

    public Variant? Variant { get; private set; }

    public VariantOption(int variantId, string name)
    {
        Id = variantId;
        Name = name;
    }
}
