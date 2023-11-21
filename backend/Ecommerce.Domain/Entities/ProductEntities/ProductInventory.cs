﻿namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductInventory : AuditableEntity
{
    public int Id { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Stock { get; private set; }

    public ProductCombination? ProductCombination { get; set; }

    private ProductInventory() { }

    public ProductInventory(Guid productId, int stock)
    {
        ProductCombinationId = productId;
        Stock = stock;
    }

    public Result ReduceStock(int quantity)
    {
        if (quantity <= 0) return Result.Fail(DomainErrors.ProductInventory.InvalidQuantity);
        if (Stock < quantity) return Result.Fail(DomainErrors.ProductInventory.InsufficientStock);

        Stock -= quantity;
        return Result.Ok();
    }
}
