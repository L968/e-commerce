namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class OrderItem
{
    public int Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Quantity { get; private set; }
    public string ProductName { get; private set; }
    public string ProductSku { get; private set; }
    public string? ProductImagePath { get; private set; }
    public decimal ProductUnitPrice { get; private set; }
    public decimal? ProductDiscount { get; private set; }

    public Order? Order { get; private set; }

    private OrderItem() { }

    public OrderItem(
        Guid orderId,
        Guid productCombinationId,
        int quantity,
        string productName,
        string productSku,
        string? productImagePath,
        decimal productUnitPrice,
        decimal? productDiscount
    )
    {
        OrderId = orderId;
        ProductCombinationId = productCombinationId;
        Quantity = quantity;
        ProductName = productName;
        ProductSku = productSku;
        ProductImagePath = productImagePath;
        ProductUnitPrice = productUnitPrice;
        ProductDiscount = productDiscount;
    }

    public decimal GetTotalAmount()
    {
        decimal discountedPrice = ProductDiscount.HasValue
            ? ProductUnitPrice - ProductDiscount.Value
            : ProductUnitPrice;

        return discountedPrice * Quantity;
    }

    public decimal GetTotalDiscount()
    {
        return (ProductDiscount ?? 0) * Quantity;
    }
}
