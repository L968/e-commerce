namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductInventory : AuditableEntity
{
    public int Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Stock { get; private set; }

    public Product? Product { get; set; }

    public ProductInventory(Guid productId, int stock)
    {
        ProductId = productId;
        Stock = stock;
    }
}