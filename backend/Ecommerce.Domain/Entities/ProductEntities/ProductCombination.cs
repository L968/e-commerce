using Ecommerce.Domain.Entities.VariantEntities;
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

    public Product Product { get; private set; }
    public ProductInventory Inventory { get; private set; }

    private readonly List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images => _images;

    private ProductCombination() { }

    public ProductCombination(
        Guid productId,
        string sku,
        decimal price,
        int stock,
        float length,
        float width,
        float height,
        float weight,
        IEnumerable<string> imagePaths,
        IEnumerable<VariantOption> variantOptions
    )
    {
        ValidateDomain(price, imagePaths, length, width, height, weight);

        string combinationString = GenerateCombinationString(variantOptions);

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

    public void Update(
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
        ValidateDomain(price, imagePaths, length, width, height, weight);

        Product.RemoveVariantOptionsByCombination(Id);
        Product.AddVariantOptions(variantOptions.Select(vo => vo.Id));

        var existingCombinations = Product.Combinations.Where(pc => pc.Id != Id);

        string combinationString = GenerateCombinationString(variantOptions);
        bool combinationAlreadyExists = existingCombinations.Any(pc => pc.CombinationString == combinationString);

        if (combinationAlreadyExists)
            throw new DomainException(DomainErrors.ProductCombination.CombinationAlreadyExists);

        CombinationString = combinationString;
        Sku = sku;
        Price = price;
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        _images.Clear();
        _images.AddRange(imagePaths.Select(i => new ProductImage(Id, i)).ToList());
    }

    public decimal GetDiscount()
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
                    throw new DomainException(DomainErrors.Order.DiscountUnitNotImplemented);
            }
        }

        return productDiscount;
    }

    public decimal GetDiscountedPrice()
    {
        decimal discountedPrice = Price - GetDiscount();
        return Math.Round(discountedPrice, 2, MidpointRounding.AwayFromZero);
    }

    private static string GenerateCombinationString(IEnumerable<VariantOption> variantOptions)
    {
        IEnumerable<string> combinationStrings = variantOptions.Select(vo => $"{vo.Variant!.Name}={vo.Name}");
        return string.Join("/", combinationStrings);
    }

    private static void ValidateDomain(decimal price, IEnumerable<string> imagePaths, float length, float width, float height, float weight)
    {
        var errors = new List<string>();

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
            throw new DomainException(errors);
    }
}
