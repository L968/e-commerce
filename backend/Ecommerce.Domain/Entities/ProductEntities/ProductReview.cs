﻿namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductReview : AuditableEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Rating { get; private set; }
    public string? Description { get; private set; }

    public Product? Product { get; private set; }

    public ProductReview(int userId, Guid productId, int rating, string? description)
    {
        UserId = userId;
        ProductId = productId;
        Rating = rating;
        Description = description;
    }

}
