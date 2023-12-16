using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Product : AuditableEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = "";
    public string Description { get; private set; } = "";
    public bool Active { get; private set; }
    public bool Visible { get; private set; }
    public Guid ProductCategoryId { get; private set; }

    public ProductCategory? Category { get; private set; }
    public List<ProductCombination> Combinations { get; private set; } = new();
    public List<ProductVariantOption> VariantOptions { get; private set; } = new();
    public List<ProductDiscount> Discounts { get; private set; } = new();
    public List<ProductReview> Reviews { get; private set; } = new();

    private Product() { }

    private Product(
        string name,
        string description,
        bool active,
        bool visible,
        Guid productCategoryId
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Active = active;
        Visible = visible;
        ProductCategoryId = productCategoryId;
    }

    public static Result<Product> Create(
        string name,
        string description,
        bool active,
        bool visible,
        Guid productCategoryId
    )
    {
        var product = new Product(
            name,
            description,
            active,
            visible,
            productCategoryId
        );

        return Result.Ok(product);
    }

    public Result Update(
        string name,
        string description,
        bool active,
        bool visible,
        Guid productCategoryId
    )
    {
        Name = name;
        Description = description;
        Active = active;
        Visible = visible;
        ProductCategoryId = productCategoryId;
        return Result.Ok();
    }

    public float GetRating()
    {
        if (Reviews.Count == 0) return 0;

        int totalRating = Reviews.Sum(r => r.Rating);
        float averageRating = (float) totalRating / Reviews.Count;

        return averageRating;
    }
}
