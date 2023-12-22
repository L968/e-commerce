using Ecommerce.Domain.Enums;

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

    public Product Product { get; private set; } = null!;
    public ProductInventory Inventory { get; private set; } = null!;

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images;

    private ProductCombination() { }

    private ProductCombination(
        Guid productId,
        string combinationString,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        List<string> imagePaths
    )
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        CombinationString = combinationString;
        Sku = sku;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Inventory = new ProductInventory(Id, stock);
        _images = imagePaths.Select(i => new ProductImage(Id, i)).ToList();
    }

    public static Result<ProductCombination> Create(
        Guid productId,
        string combinationString,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        List<string> imagePaths
    )
    {
        var validationResult = ValidateDomain(price, imagePaths, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        var productCombination = new ProductCombination(
            productId,
            combinationString,
            sku,
            price,
            stock,
            length,
            width,
            height,
            weight,
            imagePaths
        );

        return Result.Ok(productCombination);
    }

    public Result Update(
        string sku,
        decimal price,
        float length,
        float width,
        float height,
        float weight,
        List<string> imagePaths
    )
    {
        var validationResult = ValidateDomain(price, imagePaths, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        Sku = sku;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        _images.Clear();
        _images.AddRange(imagePaths.Select(i => new ProductImage(Id, i)).ToList());
        return Result.Ok();
    }

    public Result<decimal> GetDiscount()
    {
        decimal productDiscount = 0;
        ProductDiscount? activeProductDiscount = Product.Discounts.FirstOrDefault(d => d.IsCurrentlyActive());

        if (activeProductDiscount is not null)
        {
            switch (activeProductDiscount.DiscountUnit)
            {
                case DiscountUnit.Percentage:
                    productDiscount = Price * activeProductDiscount.DiscountValue / 100;
                    break;
                case DiscountUnit.FixedAmount:
                    productDiscount = activeProductDiscount.DiscountValue;
                    break;
                default:
                    return Result.Fail(DomainErrors.Order.DiscountUnitNotImplemented);
            }
        }

        return Result.Ok(productDiscount);
    }

    public decimal GetDiscountedPrice()
    {
        decimal discountedPrice = Price - GetDiscount().Value;
        return Math.Round(discountedPrice, 2, MidpointRounding.AwayFromZero);
    }

    private static Result ValidateDomain(decimal price, List<string> imagePaths, float length, float width, float height, float weight)
    {
        var errors = new List<Error>();

        if (price <= 0)
            errors.Add(DomainErrors.Product.InvalidPriceValue);

        if (imagePaths.Count <= 0)
            errors.Add(DomainErrors.Product.EmptyImagePathList);

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
