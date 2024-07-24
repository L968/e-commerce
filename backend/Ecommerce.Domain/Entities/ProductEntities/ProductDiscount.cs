using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductDiscount : AuditableEntity
{
    public int Id { get; private set; }
    public Guid ProductId { get; private init; }
    public string Name { get; private set; }
    public decimal DiscountValue { get; private set; }
    public DiscountUnit DiscountUnit { get; private set; }
    public decimal? MaximumDiscountAmount { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime? ValidUntil { get; private set; }

    public Product? Product { get; private set; }

    private ProductDiscount() { }

    public ProductDiscount(
        Guid productId,
        string name,
        decimal discountValue,
        DiscountUnit discountUnit,
        decimal? maximumDiscountAmount,
        DateTime validFrom,
        DateTime? validUntil,
        decimal productPrice
    )
    {
        ValidateDomain(discountValue, discountUnit, maximumDiscountAmount, validFrom, validUntil, productPrice);

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
        DiscountUnit discountUnit,
        decimal? maximumDiscountAmount,
        DateTime validFrom,
        DateTime? validUntil,
        decimal productPrice
    )
    {
        if (HasExpired())
            throw new DomainException(DomainErrors.ProductDiscount.CannotUpdateExpiredDiscount);

        ValidateDomain(discountValue, discountUnit, maximumDiscountAmount, validFrom, validUntil, productPrice);

        Name = name;
        DiscountValue = discountValue;
        DiscountUnit = discountUnit;
        MaximumDiscountAmount = maximumDiscountAmount;
        ValidFrom = validFrom;
        ValidUntil = validUntil;
    }

    public bool IsCurrentlyActive()
    {
        var currentDate = DateTime.UtcNow;
        return ValidFrom <= currentDate && (ValidUntil is null || ValidUntil >= currentDate);
    }

    public bool HasExpired()
    {
        return ValidUntil is not null && ValidUntil.Value < DateTime.UtcNow;
    }

    public static bool HasOverlap(DateTime validFrom, DateTime? validUntil, IEnumerable<ProductDiscount> discounts)
    {
        foreach (ProductDiscount discount in discounts)
        {
            if (validUntil is not null && discount.ValidUntil is not null)
            {
                if (validFrom <= discount.ValidUntil.Value && validUntil >= discount.ValidFrom)
                {
                    return true;
                }
            }
            else if (validUntil is null && discount.ValidUntil is null)
            {
                return true;
            }
            else if (validUntil is null && discount.ValidUntil is not null)
            {
                if (validFrom < discount.ValidUntil.Value)
                {
                    return true;
                }
            }
            else if (validUntil is not null && discount.ValidUntil is null)
            {
                if (validUntil.Value > discount.ValidFrom)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void ValidateDomain(decimal discountValue, DiscountUnit discountUnit, decimal? maximumDiscountAmount, DateTime validFrom, DateTime? validUntil, decimal productPrice)
    {
        var errors = new List<string>();

        if (validFrom < DateTime.UtcNow)
            errors.Add(DomainErrors.ProductDiscount.DiscountStartDateInPast);

        if (validFrom >= validUntil)
            errors.Add(DomainErrors.ProductDiscount.DiscountEndDateMustBeAfterStartDate);

        if (discountValue <= 0)
            errors.Add(DomainErrors.ProductDiscount.InvalidDiscountValue);

        if (validUntil is not null && validUntil <= validFrom.AddMinutes(5))
            errors.Add(DomainErrors.ProductDiscount.DiscountDurationTooShort);

        if (discountUnit == DiscountUnit.Percentage && discountValue >= 80)
            errors.Add(DomainErrors.ProductDiscount.DiscountPercentageExceedsLimit);

        if (discountUnit == DiscountUnit.FixedAmount)
        {
            decimal maxDiscountAmount = productPrice * 0.8m;
            if (discountValue >= maxDiscountAmount)
                errors.Add(DomainErrors.ProductDiscount.MaximumFixedDiscountExceeded);
        }

        if (maximumDiscountAmount is not null
         && discountUnit == DiscountUnit.FixedAmount
         && maximumDiscountAmount >= discountValue
        )
        {
            errors.Add(DomainErrors.ProductDiscount.MaximumDiscountAmountExceedsValue);
        }

        if (errors.Count > 0)
            throw new DomainException(errors);
    }
}
