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

    public Result ReduceStock(int quantity)
    {
        var result = ValidateStock(quantity);
        if (result.IsFailed) return result;

        Stock -= quantity;
        return Result.Ok();
    }

    public Result ValidateStock(int quantity)
    {
        if (quantity <= 0) return DomainErrors.ProductInventory.InvalidQuantity;
        if (Stock < quantity) return DomainErrors.ProductInventory.InsufficientStock;

        return Result.Ok();
    }
}
