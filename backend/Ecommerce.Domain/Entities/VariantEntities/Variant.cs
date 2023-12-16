namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class Variant
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public List<ProductCategoryVariant>? ProductCategoryVariants { get; private set; } = new();

    private readonly List<VariantOption> _options = new();
    public IReadOnlyCollection<VariantOption> Options => _options;

    private Variant() { }

    public Variant(string name)
    {
        Name = name;
    }

    public Result Update(string name, IEnumerable<string> options)
    {
        Name = name;
        return SetOptions(options);
    }

    private Result SetOptions(IEnumerable<string> options)
    {
        if(!options.Any())
            return Result.Fail(DomainErrors.Variant.EmptyOptionList);

        _options.Clear();

        var variantOptions = options.Select(option => new VariantOption(Id, option));

        _options.AddRange(variantOptions);

        return Result.Ok();
    }
}
