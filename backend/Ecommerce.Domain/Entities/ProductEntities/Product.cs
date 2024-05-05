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
    public List<ProductDiscount> Discounts { get; private set; } = [];

    private readonly List<ProductCombination> _combinations = [];
    public IReadOnlyCollection<ProductCombination> Combinations => _combinations;

    private readonly List<ProductReview> _reviews = [];
    public IReadOnlyCollection<ProductReview> Reviews => _reviews;

    private readonly List<ProductVariantOption> _variantOptions = [];
    public IReadOnlyCollection<ProductVariantOption> VariantOptions => _variantOptions;

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

    public Result<ProductCombination> AddCombination(
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
        var result = ProductCombination.Create(
            Id,
            variantOptions,
            sku,
            price,
            stock,
            length,
            width,
            height,
            weight,
            imagePaths
        );

        if (result.IsFailed) return result;

        ProductCombination productCombination = result.Value;
        bool combinationAlreadyExists = Combinations.Any(pc => pc.CombinationString == productCombination.CombinationString);

        if (combinationAlreadyExists)
            return DomainErrors.ProductCombination.CombinationAlreadyExists;

        AddVariantOptions(variantOptions.Select(v => v.Id));

        _combinations.Add(productCombination);
        return productCombination;
    }

    public Result<ProductReview> AddReview(int userId, int rating, string? description)
    {
        var result = ProductReview.Create(userId, Id, rating, description);
        if (result.IsFailed) return result;

        _reviews.Add(result.Value);

        return result.Value;
    }

    /// <summary>
    /// Calculates the average rating of the product based on its reviews.
    /// If there are no reviews, returns 0.
    /// </summary>
    /// <returns>The average rating of the product.</returns>
    public float GetRating()
    {
        if (Reviews.Count == 0) return 0;

        int totalRating = Reviews.Sum(r => r.Rating);
        float averageRating = (float) totalRating / Reviews.Count;

        return averageRating;
    }

    /// <summary>
    /// Adds variant options to the product if they are not already associated with it.
    /// </summary>
    /// <param name="variantOptionIds">The IDs of the variant options to add.</param>
    public void AddVariantOptions(IEnumerable<int> variantOptionIds)
    {
        foreach (int variantOptionId in variantOptionIds)
        {
            bool alreadyExists = _variantOptions.Any(existingVo => existingVo.VariantOptionId == variantOptionId);
            if (alreadyExists) continue;

            var productVariantOption = new ProductVariantOption(Id, variantOptionId);
            _variantOptions.Add(productVariantOption);
        }
    }

    /// <summary>
    /// Removes variant options associated with a specific combination from the product.<br/>
    /// Variant options are only removed if they are not used by other combinations of the same product.
    /// </summary>
    /// <param name="productCombinationId">The ID of the combination to remove variants from.</param>
    public void RemoveVariantOptionsByCombination(Guid productCombinationId)
    {
        List<VariantOption> variantOptions = VariantOptions.Select(pvo => pvo.VariantOption!).ToList();
        List<VariantOption> variantOptionsToDelete = new(variantOptions);

        foreach (ProductCombination combination in Combinations)
        {
            if (combination.Id == productCombinationId) continue;

            foreach (var variantOption in variantOptions)
            {
                string combinationStringFragment = $"{variantOption.Variant!.Name}={variantOption.Name}";

                if (combination.CombinationString.Contains(combinationStringFragment))
                {
                    variantOptionsToDelete.Remove(variantOption);
                }
            }
        }

        foreach (var variantOptionToDelete in variantOptionsToDelete)
        {
            ProductVariantOption? variantOption = _variantOptions.FirstOrDefault(pvo => pvo.VariantOptionId == variantOptionToDelete.Id);
            if (variantOption != null) _variantOptions.Remove(variantOption);
        }
    }
}
