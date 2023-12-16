namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class Variant
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public List<VariantOption>? Options { get; private set; }
    public List<ProductCategoryVariant>? ProductCategoryVariants { get; private set; }

    private Variant() { }

    public Variant(string name)
    {
        Name = name;
    }
}
