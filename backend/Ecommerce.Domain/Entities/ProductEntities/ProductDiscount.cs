namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductDiscount : AuditableEntity
{
    public int Id { get; private set; }
    public Guid ProductId { get; private init; }
    public string Name { get; private set; }
    public decimal DiscountValue { get; private set; }
    public string DiscountUnit { get; private set; }
    public decimal? MaximumDiscountAmount { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime? ValidUntil { get; private set; }

    public Product? Product { get; set; }

    public ProductDiscount(
        Guid productId,
        string name,
        decimal discountValue,
        string discountUnit,
        decimal? maximumDiscountAmount,
        DateTime validFrom,
        DateTime? validUntil
    )
    {
        ProductId = productId;
        Name = name;
        DiscountValue = discountValue;
        DiscountUnit = discountUnit;
        MaximumDiscountAmount = maximumDiscountAmount;
        ValidFrom = validFrom;
        ValidUntil = validUntil;
    }

    public void Update(
        string name,
        decimal discountValue,
        string discountUnit,
        decimal? maximumDiscountAmount,
        DateTime validFrom,
        DateTime? validUntil
    )
    {
        Name = name;
        DiscountValue = discountValue;
        DiscountUnit = discountUnit;
        MaximumDiscountAmount = maximumDiscountAmount;
        ValidFrom = validFrom;
        ValidUntil = validUntil;
    }

    public bool IsCurrentlyValid()
    {
        var currentDate = DateTime.UtcNow;
        return ValidFrom <= currentDate && (ValidUntil == null || ValidUntil >= currentDate);
    }
}