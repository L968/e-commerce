namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class OrderHistory
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }
}