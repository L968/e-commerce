namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class VariantOption
{
    public int Id { get; private set; }
    public int VariantId { get; private set; }
    public string Name { get; private set; }

    private VariantOption(int variantId, string name)
    {
        Id = variantId;
        Name = name;
    }

    public static Result<VariantOption> Create(int variantId, string name)
    {
        return Result.Ok(new VariantOption(variantId, name));
    }
}
