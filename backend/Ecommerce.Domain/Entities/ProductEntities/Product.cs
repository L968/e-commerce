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

    public Product(
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
        ValidateDomain(price, length, width, height, weight);

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

    public void Update(
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
        ValidateDomain(price, length, width, height, weight);

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

    private void ValidateDomain(decimal price, float length, float width, float height, float weight)
    {
        DomainExceptionValidation.When(price < 0,
            "Invalid price value");

        DomainExceptionValidation.When(length <= 0,
            "Invalid length value");

        DomainExceptionValidation.When(width <= 0,
            "Invalid width value");

        DomainExceptionValidation.When(height <= 0,
            "Invalid height value");

        DomainExceptionValidation.When(weight <= 0,
            "Invalid weight value");
    }
}