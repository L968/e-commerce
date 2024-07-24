using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Entities.VariantEntities;
using Ecommerce.Domain.UnitTests.SampleData;

namespace Ecommerce.Domain.UnitTests.Products;

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
        var product = new Product(name, description, active, visible, productCategoryId);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.Equal(active, product.Active);
        Assert.Equal(visible, product.Visible);
        Assert.Equal(productCategoryId, product.ProductCategoryId);
    }

    [Fact]
    public void GivenValidData_ShouldUpdateProduct()
    {
        // Arrange
        var product = FakeProducts.GetProduct();
        var newName = "New T-Shirt";
        var newDescription = "Brand new T-Shirt";
        var newActive = false;
        var newVisible = true;
        var newProductCategoryId = Guid.NewGuid();

        // Act
        product.Update(newName, newDescription, newActive, newVisible, newProductCategoryId);

        // Assert
        Assert.Equal(newName, product.Name);
        Assert.Equal(newDescription, product.Description);
        Assert.Equal(newActive, product.Active);
        Assert.Equal(newVisible, product.Visible);
        Assert.Equal(newProductCategoryId, product.ProductCategoryId);
    }

    [Fact]
    public void GivenValidData_ShouldAddReview()
    {
        // Arrange
        Product product = FakeProducts.GetProduct();
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
        Product product = FakeProducts.GetProduct();

        // Act
        float averageRating = product.GetRating();

        // Assert
        Assert.Equal(0, averageRating);
    }

    [Theory]
    [InlineData([new int[] { 4, 5 }, 4.5])]
    [InlineData([new int[] { 3, 3, 3 }, 3])]
    [InlineData([new int[] { 1, 2, 3, 4, 5 }, 3])]
    [InlineData([new int[] { 5, 5, 5, 5 }, 5])]
    public void GivenMultipleReviews_ShouldCalculateAverageRating(int[] ratings, float expectedAverage)
    {
        // Arrange
        Product product = FakeProducts.GetProduct();

        foreach (var rating in ratings)
        {
            product.AddReview(1, rating, null);
        }

        // Act
        float averageRating = product.GetRating();

        // Assert
        Assert.Equal(expectedAverage, averageRating);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void GivenVariantOptions_ShouldAddVariantOptions(int numOptions)
    {
        // Arrange
        Product product = FakeProducts.GetProduct();
        IEnumerable<VariantOption> variantOptions = FakeVariants.GetVariantOptions(numOptions);

        // Act
        product.AddVariantOptions(variantOptions.Select(vo => vo.Id));

        // Assert
        Assert.Equal(numOptions, product.VariantOptions.Count);
    }

    [Fact]
    public void GivenVariantOptionsToRemove_ShouldRemoveVariantOptionsByCombination()
    {
        // Arrange
        Product product = FakeProducts.GetProduct(1);

        // Act
        product.RemoveVariantOptionsByCombination(product.Combinations.ElementAt(0).Id);

        // Assert
        Assert.Empty(product.VariantOptions);
    }
}
