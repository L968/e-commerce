using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Entities.VariantEntities;
using Ecommerce.Domain.UnitTests.SampleData;
using FluentResults;

namespace Ecommerce.Domain.UnitTests;

public class ProductTests
{
    [Fact]
    public void GivenValidData_ShouldCreateProduct()
    {
        // Arrange
        var name = "T-Shirt";
        var description = "Brand new T-Shirt";
        var active = true;
        var visible = true;
        var productCategoryId = Guid.NewGuid();

        // Act
        Result<Product> result = Product.Create(name, description, active, visible, productCategoryId);

        // Assert
        Assert.False(result.IsFailed);
        Assert.NotNull(result.Value);
        Assert.Equal(name, result.Value.Name);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(active, result.Value.Active);
        Assert.Equal(visible, result.Value.Visible);
        Assert.Equal(productCategoryId, result.Value.ProductCategoryId);
    }

    [Fact]
    public void GivenValidData_ShouldAddReview()
    {
        // Arrange
        Product product = FakeProducts.GetProducts();
        Utils.SetPrivateProperty(product, "_reviews", new List<ProductReview>());
        var userId = 1;
        var rating = 5;
        var description = "Great product!";

        // Act
        product.AddReview(userId, rating, description);

        // Assert
        Assert.Single(product.Reviews);
        Assert.Equal(userId, product.Reviews.ElementAt(0).UserId);
        Assert.Equal(rating, product.Reviews.ElementAt(0).Rating);
        Assert.Equal(description, product.Reviews.ElementAt(0).Description);
    }

    [Fact]
    public void GivenNoReviews_ShouldReturnZeroRating()
    {
        // Arrange
        Product product = FakeProducts.GetProducts();
        Utils.SetPrivateProperty(product, "_reviews", new List<ProductReview>());

        // Act
        float averageRating = product.GetRating();

        // Assert
        Assert.Equal(0, averageRating);
    }

    [Fact]
    public void GivenMultipleReviews_ShouldCalculateAverageRating()
    {
        // Arrange
        Product product = FakeProducts.GetProducts();
        Utils.SetPrivateProperty(product, "_reviews", new List<ProductReview>());
        product.AddReview(1, 4, null);
        product.AddReview(2, 5, null);

        // Act
        float averageRating = product.GetRating();

        // Assert
        Assert.Equal(4.5, averageRating);
    }

    [Fact]
    public void GivenVariantOptions_ShouldAddVariantOptions()
    {
        // Arrange
        Product product = FakeProducts.GetProducts();
        IEnumerable<VariantOption> variantOptions = FakeVariants.GetVariantOptions();

        // Act
        product.AddVariantOptions(variantOptions.Select(vo => vo.Id));

        // Assert
        Assert.Equal(2, product.VariantOptions.Count);
    }

    [Fact]
    public void GivenVariantOptionsToRemove_ShouldRemoveVariantOptionsByCombination()
    {
        // Arrange
        Product product = FakeProducts.GetProducts();

        // Act
        product.RemoveVariantOptionsByCombination(product.Combinations.ElementAt(0).Id);

        // Assert
        Assert.Empty(product.VariantOptions);
    }
}
