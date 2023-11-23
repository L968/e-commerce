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

    private OrderItem() { }

    public OrderItem(
        Guid orderId,
        Guid productCombinationId,
        string productName,
        string productSku,
        string? productImagePath,
        decimal productUnitPrice,
        decimal? productDiscount
    )
    {
        OrderId = orderId;
        ProductCombinationId = productCombinationId;
        ProductName = productName;
        ProductSku = productSku;
        ProductImagePath = productImagePath;
        ProductUnitPrice = productUnitPrice;
        ProductDiscount = productDiscount;
    }

    public void SetOrderId(Guid orderId)
    {
        if (OrderId == Guid.Empty)
        {
            OrderId = orderId;
        }
    }
}
