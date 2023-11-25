namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class Variant
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Variant(string name)
    {
        Name = name;
    }
}
