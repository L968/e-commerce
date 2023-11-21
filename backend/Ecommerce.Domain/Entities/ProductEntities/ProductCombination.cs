namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductCombination : AuditableEntity
{
    public Guid Id { get; private init; }
    public Guid ProductId { get; private set; }
    public string CombinationString { get; private set; }
    public string Sku { get; private set; }
    public decimal Price { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }

    public ProductInventory Inventory { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images;

    private ProductCombination() { }

    private ProductCombination(
        string combinationString,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        List<ProductImage> images
    )
    {
        Id = Guid.NewGuid();
        CombinationString = combinationString;
        Sku = sku;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Inventory = new ProductInventory(Id, stock);
        _images = images;
    }

    public static Result<ProductCombination> Create(
        string combinationString,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        List<ProductImage> images
    )
    {
        var validationResult = ValidateDomain(price, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        var productCombination = new ProductCombination(
            combinationString,
            sku,
            price,
            stock,
            length,
            width,
            height,
            weight,
            images
        );

        return Result.Ok(productCombination);
    }

    public Result Update(
        string sku,
        decimal price,
        float length,
        float width,
        float height,
        float weight
    )
    {
        var validationResult = ValidateDomain(price, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        Sku = sku;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
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

        if (price <= 0)
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
