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
        var productReview = new ProductReview(userId, productId, rating, description);

        // Assert
        Assert.Equal(userId, productReview.UserId);
        Assert.Equal(productId, productReview.ProductId);
        Assert.Equal(rating, productReview.Rating);
        Assert.Equal(description, productReview.Description);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithInvalidUserId_ShouldFail(int userId)
    {
        // Arrange
        var productId = _faker.Random.Guid();
        var rating = _faker.Random.Number(1, 5);
        var description = _faker.Lorem.Sentence();

        // Act and Assert
        var exception = Assert.Throws<DomainException>(() => new ProductReview(userId, productId, rating, description));
        Assert.Contains(DomainErrors.ProductReview.InvalidUserId, exception.Errors);
    }

    [Fact]
    public void Create_WithInvalidProductId_ShouldFail()
    {
        // Arrange
        var userId = _faker.Random.Number(1, 1000);
        var productId = Guid.Empty;
        var rating = _faker.Random.Number(1, 5);
        var description = _faker.Lorem.Sentence();

        // Act and Assert
        var exception = Assert.Throws<DomainException>(() => new ProductReview(userId, productId, rating, description));
        Assert.Contains(DomainErrors.ProductReview.InvalidProductId, exception.Errors);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Create_WithInvalidRating_ShouldFail(int rating)
    {
        // Arrange
        var userId = _faker.Random.Number(1, 1000);
        var productId = _faker.Random.Guid();
        var description = _faker.Lorem.Sentence();

        // Act and Assert
        var exception = Assert.Throws<DomainException>(() => new ProductReview(userId, productId, rating, description));
        Assert.Contains(DomainErrors.ProductReview.InvalidRatingRange, exception.Errors);
    }
}
