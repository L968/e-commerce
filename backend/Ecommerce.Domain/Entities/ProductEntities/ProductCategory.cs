using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductCategory : AuditableEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = "";
    public string? Description { get; private set; }

    private readonly List<ProductCategoryVariant> _variants = [];
    public IReadOnlyCollection<ProductCategoryVariant> Variants => _variants;

    private ProductCategory() { }

    public ProductCategory(string name, string? description, IEnumerable<Guid> variantIds)
    {
        ValidateDomain(variantIds);

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        SetVariants(variantIds);
    }

    public void Update(string name, string? description, IEnumerable<Guid> variantIds)
    {
        ValidateDomain(variantIds);

        Name = name;
        Description = description;
        SetVariants(variantIds);
    }

    private void SetVariants(IEnumerable<Guid> variantIds)
    {
        _variants.Clear();
        _variants.AddRange(variantIds.Select(variantId => new ProductCategoryVariant(Id, variantId)));
    }

    private static void ValidateDomain(IEnumerable<Guid> variantIds)
    {
        var errors = new List<string>();

        if (!variantIds.Any())
            errors.Add(DomainErrors.ProductCategory.EmptyVariantList);

        if (errors.Count > 0)
            throw new DomainException(errors);
    }
}
