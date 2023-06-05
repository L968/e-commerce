namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductInventory : BaseEntity
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Product? Product { get; set; }
}