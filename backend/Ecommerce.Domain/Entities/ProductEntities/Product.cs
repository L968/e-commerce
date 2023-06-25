namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Product : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string Sku { get; private set; } = "";
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
    public bool Visible { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public int ProductCategoryId { get; private set; }
    public ProductInventory Inventory { get; private set; } = null!;

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images;

    public ProductCategory? Category { get; set; }
    public ICollection<ProductDiscount>? Discounts { get; set; }

    private Product() { }

    private Product(
        string name,
        string description,
        string sku,
        decimal price,
        bool active,
        bool visible,
        float length,
        float width,
        float height,
        float weight,
        int productCategoryId,
        int stock,
        List<ProductImage> images
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
        Active = active;
        Visible = visible;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        ProductCategoryId = productCategoryId;
        Inventory = new ProductInventory(Id, stock);
        _images = images;
    }

    public static Result<Product> Create(
        string name,
        string description,
        string sku,
        decimal price,
        bool active,
        bool visible,
        float length,
        float width,
        float height,
        float weight,
        int productCategoryId,
        int stock,
        List<ProductImage> images
    )
    {
        var validationResult = ValidateDomain(price, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        var product = new Product(
            name,
            description,
            sku,
            price,
            active,
            visible,
            length,
            width,
            height,
            weight,
            productCategoryId,
            stock,
            images
        );

        return Result.Ok(product);
    }

    public Result Update(
        string name,
        string description,
        string sku,
        decimal price,
        bool active,
        bool visible,
        float length,
        float width,
        float height,
        float weight,
        int productCategoryId
    )
    {
        var validationResult = ValidateDomain(price, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
        Active = active;
        Visible = visible;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        ProductCategoryId = productCategoryId;
        return Result.Ok();
    }

    public void AddImage(ProductImage image)
    {
        _images.Add(image);
    }

    public void RemoveImage(int productImageId)
    {
        var image = _images.FirstOrDefault(i => i.Id == productImageId);

        if (image is not null)
        {
            _images.Remove(image);
        }
    }

    private static Result ValidateDomain(decimal price, float length, float width, float height, float weight)
    {
        var errors = new List<Error>();

        if (price < 0)
            errors.Add(DomainErrors.Product.InvalidPriceValue);

        if (length <= 0)
            errors.Add(DomainErrors.Product.InvalidLengthValue);

        if (width <= 0)
            errors.Add(DomainErrors.Product.InvalidWidthValue);

        if (height <= 0)
            errors.Add(DomainErrors.Product.InvalidHeightValue);

        if (weight <= 0)
            errors.Add(DomainErrors.Product.InvalidWeightValue);

        if (errors.Any())
            return Result.Fail(errors);

        return Result.Ok();
    }
}