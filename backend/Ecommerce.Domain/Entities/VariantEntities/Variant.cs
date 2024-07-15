namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class Variant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private readonly List<ProductCategoryVariant> _productCategoryVariants = [];
    public IReadOnlyCollection<ProductCategoryVariant> ProductCategoryVariants => _productCategoryVariants;

    private readonly List<VariantOption> _options = [];
    public IReadOnlyCollection<VariantOption> Options => _options;

    private Variant() { }

    public Variant(string name, List<string> options)
    {
        ValidateDomain(options);

        Id = Guid.NewGuid();
        Name = name;
        SetOptions(options);
    }

    public void Update(string name, IEnumerable<string> options)
    {
        ValidateDomain(options);

        Name = name;
        SetOptions(options);
    }

    private void SetOptions(IEnumerable<string> options)
    {
        _options.Clear();
        _options.AddRange(options.Select(option => new VariantOption(Id, option)));
    }

    private static void ValidateDomain(IEnumerable<string> options)
    {
        var errors = new List<string>();

        if (!options.Any())
            errors.Add(DomainErrors.Variant.EmptyOptionList);

        if (errors.Count > 0)
            throw new DomainException(errors);
    }
}
