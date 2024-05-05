namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductReview : AuditableEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Rating { get; private set; }
    public string? Description { get; private set; }

    public Product? Product { get; private set; }

    private ProductReview() { }

    private ProductReview(int userId, Guid productId, int rating, string? description)
    {
        UserId = userId;
        ProductId = productId;
        Rating = rating;
        Description = description;
    }

    public static Result<ProductReview> Create(int userId, Guid productId, int rating, string? description)
    {
        if (userId <= 0)
            return DomainErrors.ProductReview.InvalidUserId;

        if (productId == Guid.Empty)
            return DomainErrors.ProductReview.InvalidProductId;

        if (rating < 1 || rating > 5)
            return DomainErrors.ProductReview.InvalidRatingRange;

        return Result.Ok(new ProductReview(userId, productId, rating, description));
    }
}
