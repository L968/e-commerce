namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductInventory : AuditableEntity
{
    public int Id { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Stock { get; private set; }

    public ProductCombination? ProductCombination { get; private set; }

    private ProductInventory() { }

    public ProductInventory(Guid productId, int stock)
    {
        ProductCombinationId = productId;
        Stock = stock;
    }

    public void ReduceStock(int quantity)
    {
        ValidateStock(quantity);
        Stock -= quantity;
    }

    public void ValidateStock(int quantity)
    {
        if (quantity <= 0) throw new DomainException(DomainErrors.ProductInventory.InvalidQuantity);
        if (Stock < quantity) throw new DomainException( DomainErrors.ProductInventory.InsufficientStock);
    }
}
