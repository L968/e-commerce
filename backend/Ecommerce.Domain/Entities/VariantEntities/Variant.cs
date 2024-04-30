namespace Ecommerce.Domain.Entities.VariantEntities;

public sealed class Variant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public List<ProductCategoryVariant>? ProductCategoryVariants { get; private set; } = [];

    private readonly List<VariantOption> _options = [];
    public IReadOnlyCollection<VariantOption> Options => _options;

    private Variant() { }

    private Variant(string name, List<string> options)
    {
        Id = Guid.NewGuid();
        Name = name;
        SetOptions(options);
    }

    public static Result<Variant> Create(string name, List<string> options)
    {
        var validation = ValidateDomain(options);

        if (validation.IsFailed)
            return validation;

        return Result.Ok(new Variant(name, options));
    }

    public Result Update(string name, IEnumerable<string> options)
    {
        var validation = ValidateDomain(options);

        if (validation.IsFailed)
            return validation;

        Name = name;
        SetOptions(options);

        return Result.Ok();
    }

    private void SetOptions(IEnumerable<string> options)
    {
        _options.Clear();
        _options.AddRange(options.Select(option => new VariantOption(Id, option)));
    }

    private static Result ValidateDomain(IEnumerable<string> options)
    {
        var errors = new List<Error>();

        if (!options.Any())
            return DomainErrors.Variant.EmptyOptionList;

        if (errors.Count != 0)
            return Result.Fail(errors);

        return Result.Ok();
    }
}
