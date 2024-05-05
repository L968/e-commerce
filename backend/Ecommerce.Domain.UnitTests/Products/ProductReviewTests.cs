using Bogus;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Errors;

namespace Ecommerce.Domain.UnitTests.Products;

public class ProductReviewTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Create_WithValidData_ShouldCreateProductReview()
    {
        // Arrange
        var userId = _faker.Random.Number(1, 1000);
        var productId = _faker.Random.Guid();
        var rating = _faker.Random.Number(1, 5);
        var description = _faker.Lorem.Sentence();

        // Act
        var result = ProductReview.Create(userId, productId, rating, description);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        var productReview = result.Value;
        Assert.Equal(userId, productReview.UserId);
        Assert.Equal(productId, productReview.ProductId);
        Assert.Equal(rating, productReview.Rating);
        Assert.Equal(description, productReview.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithInvalidUserId_ShouldReturnError(int userId)
    {
        // Arrange
        var productId = _faker.Random.Guid();
        var rating = _faker.Random.Number(1, 5);
        var description = _faker.Lorem.Sentence();

        // Act
        var result = ProductReview.Create(userId, productId, rating, description);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.ProductReview.InvalidUserId, result.Errors[0]);
    }

    [Fact]
    public void Create_WithInvalidProductId_ShouldReturnError()
    {
        // Arrange
        var userId = _faker.Random.Number(1, 1000);
        var productId = Guid.Empty;
        var rating = _faker.Random.Number(1, 5);
        var description = _faker.Lorem.Sentence();

        // Act
        var result = ProductReview.Create(userId, productId, rating, description);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.ProductReview.InvalidProductId, result.Errors[0]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Create_WithInvalidRating_ShouldReturnError(int rating)
    {
        // Arrange
        var userId = _faker.Random.Number(1, 1000);
        var productId = _faker.Random.Guid();
        var description = _faker.Lorem.Sentence();

        // Act
        var result = ProductReview.Create(userId, productId, rating, description);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.ProductReview.InvalidRatingRange, result.Errors[0]);
    }
}
