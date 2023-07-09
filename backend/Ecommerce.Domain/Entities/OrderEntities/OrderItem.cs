namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class OrderItem
{
    public int Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; private set; }
    public string ProductSku { get; private set; }
    public string? ProductImagePath { get; private set; }
    public decimal ProductPrice { get; private set; }
    public decimal? ProductDiscount { get; private set; }
}