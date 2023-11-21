namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Variant
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private Variant(string name)
    {
        Name = name;
    }

    public static Result<Variant> Create(string name)
    {
        return Result.Ok(new Variant(name));
    }
}
