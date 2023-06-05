namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductReview : BaseEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int ProductId { get; private set; }
    public int Rating { get; private set; }
    public string? Description { get; private set; }
    public bool Anonymous { get; private set; }
}