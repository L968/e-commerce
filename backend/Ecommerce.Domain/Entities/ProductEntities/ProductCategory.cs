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

    public ProductCategory(string name, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public Result Update(string name, string? description, IEnumerable<int> variantIds)
    {
        Name = name;
        Description = description;
        return SetVariants(variantIds);
    }

    private Result SetVariants(IEnumerable<int> variantIds)
    {
        if (!variantIds.Any())
            return Result.Fail(DomainErrors.ProductCategory.EmptyVariantList);

        _variants.Clear();

        var productCategoryVariants = variantIds.Select(variantId => new ProductCategoryVariant(Id, variantId));

        _variants.AddRange(productCategoryVariants);

        return Result.Ok();
    }
}
