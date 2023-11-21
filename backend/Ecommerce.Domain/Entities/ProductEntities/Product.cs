namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Product : AuditableEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = "";
    public string Description { get; private set; } = "";
    public bool Active { get; private set; }
    public bool Visible { get; private set; }
    public int ProductCategoryId { get; private set; }

    public ProductCategory? Category { get; private set; }
    public List<ProductDiscount> Discounts { get; private set; } = new();
    public List<ProductCombination> Combinations { get; private set; } = new();

    private Product() { }

    private Product(
        string name,
        string description,
        bool active,
        bool visible,
        int productCategoryId
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
        int productCategoryId
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
        int productCategoryId
    )
    {
        Name = name;
        Description = description;
        Active = active;
        Visible = visible;
        ProductCategoryId = productCategoryId;

        return Result.Ok();
    }
}
