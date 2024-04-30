using Ecommerce.Domain.Entities.VariantEntities;
using Ecommerce.Domain.Enums;
using static Ecommerce.Domain.Errors.DomainErrors;

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

    public Product Product { get; private set; }
    public ProductInventory Inventory { get; private set; }

    private readonly List<ProductImage> _images = [];
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
        IEnumerable<string> imagePaths
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
        IEnumerable<VariantOption> variantOptions,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        IEnumerable<string> imagePaths
    )
    {
        var validationResult = ValidateDomain(price, imagePaths, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        string combinationString = GenerateCombinationString(variantOptions);

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
        IEnumerable<VariantOption> variantOptions,
        string sku,
        decimal price,
        float length,
        float width,
        float height,
        float weight,
        IEnumerable<string> imagePaths
    )
    {
        Result validationResult = ValidateDomain(price, imagePaths, length, width, height, weight);
        if (validationResult.IsFailed) return validationResult;

        Product.RemoveVariantOptionsByCombination(Id);
        Product.AddVariantOptions(variantOptions.Select(vo => vo.Id));

        var existingCombinations = Product.Combinations.Where(pc => pc.Id != Id);

        string combinationString = GenerateCombinationString(variantOptions);
        bool combinationAlreadyExists = existingCombinations.Any(pc => pc.CombinationString == combinationString);

        if (combinationAlreadyExists)
            return Result.Fail(DomainErrors.ProductCombination.CombinationAlreadyExists);

        CombinationString = combinationString;
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

    private static string GenerateCombinationString(IEnumerable<VariantOption> variantOptions)
    {
        IEnumerable<string> combinationStrings = variantOptions.Select(vo => $"{vo.Variant!.Name}={vo.Name}");
        return string.Join("/", combinationStrings);
    }

    private static Result ValidateDomain(decimal price, IEnumerable<string> imagePaths, float length, float width, float height, float weight)
    {
        var errors = new List<Error>();

        if (price <= 0)
            errors.Add(DomainErrors.Product.InvalidPriceValue);

        if (!imagePaths.Any())
            errors.Add(DomainErrors.Product.EmptyImagePathList);

        if (length <= 0)
            errors.Add(DomainErrors.Product.InvalidLengthValue);

        if (width <= 0)
            errors.Add(DomainErrors.Product.InvalidWidthValue);

        if (height <= 0)
            errors.Add(DomainErrors.Product.InvalidHeightValue);

        if (weight <= 0)
            errors.Add(DomainErrors.Product.InvalidWeightValue);

        if (errors.Count > 0)
            return Result.Fail(errors);

        return Result.Ok();
    }
}
