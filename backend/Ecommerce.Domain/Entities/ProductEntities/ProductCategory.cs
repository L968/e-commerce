using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductCategory : AuditableEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = "";
    public string? Description { get; private set; }

    private readonly List<ProductCategoryVariant> _variants = new();
    public IReadOnlyCollection<ProductCategoryVariant> Variants => _variants;

    private ProductCategory() {}

    private ProductCategory(string name, string? description, IEnumerable<Guid> variantIds)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        SetVariants(variantIds);
    }

    public static Result<ProductCategory> Create(string name, string? description, IEnumerable<Guid> variantIds)
    {
        var result = ValidateDomain(variantIds);

        if (result.IsFailed)
            return result;

        return Result.Ok(new ProductCategory(name, description, variantIds));
    }

    public Result Update(string name, string? description, IEnumerable<Guid> variantIds)
    {
        var result = ValidateDomain(variantIds);

        if (result.IsFailed)
            return result;

        Name = name;
        Description = description;
        SetVariants(variantIds);

        return Result.Ok();
    }

    private Result SetVariants(IEnumerable<Guid> variantIds)
    {
        _variants.Clear();
        _variants.AddRange(variantIds.Select(variantId => new ProductCategoryVariant(Id, variantId)));

        return Result.Ok();
    }

    private static Result ValidateDomain(IEnumerable<Guid> variantIds)
    {
        var errors = new List<Error>();

        if (!variantIds.Any())
            return Result.Fail(DomainErrors.ProductCategory.EmptyVariantList);

        if (errors.Any())
            return Result.Fail(errors);

        return Result.Ok();
    }
}
