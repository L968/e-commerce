namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Payment : AuditableEntity
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public decimal Value { get; private set; }
    public string Currency { get; private set; }
    public string Method { get; private set; }
    public string Status { get; private set; }
    public DateTime PaymentDate { get; private set; }
}
