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
    public Product? Product { get; set; }

    private ProductDiscount(
        Guid productId,
        string name,
        decimal discountValue,
        DiscountUnit discountUnit,
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

    public static Result<ProductDiscount> Create(
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
        var validationResult = ValidateDomain(discountValue, discountUnit, maximumDiscountAmount, validFrom, validUntil, productPrice);
        if (validationResult.IsFailed) return validationResult;

        return Result.Ok(new ProductDiscount(
            productId,
            name,
            discountValue,
            discountUnit,
            maximumDiscountAmount,
            validFrom,
            validUntil
        ));
    }

    public Result Update(
        string name,
        decimal discountValue,
        DiscountUnit discountUnit,
        decimal? maximumDiscountAmount,
        DateTime validFrom,
        DateTime? validUntil,
        decimal productPrice
    )
    {
        if (HasExpired()) return Result.Fail(DomainErrors.ProductDiscount.CannotUpdateExpiredDiscount);

        var validationResult = ValidateDomain(discountValue, discountUnit, maximumDiscountAmount, validFrom, validUntil, productPrice);
        if (validationResult.IsFailed) return validationResult;

        Name = name;
        DiscountValue = discountValue;
        DiscountUnit = discountUnit;
        MaximumDiscountAmount = maximumDiscountAmount;
        ValidFrom = validFrom;
        ValidUntil = validUntil;
        return Result.Ok();
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

    private static Result ValidateDomain(decimal discountValue, DiscountUnit discountUnit, decimal? maximumDiscountAmount, DateTime validFrom, DateTime? validUntil, decimal productPrice)
    {
        if (validFrom < DateTime.UtcNow) return Result.Fail(DomainErrors.ProductDiscount.DiscountStartDateInPast);

        if (validFrom >= validUntil) return Result.Fail(DomainErrors.ProductDiscount.DiscountEndDateMustBeAfterStartDate);

        if (discountValue <= 0) return Result.Fail(DomainErrors.ProductDiscount.InvalidDiscountValue);

        if (validUntil is not null && validUntil <= validFrom.AddMinutes(5)) return Result.Fail(DomainErrors.ProductDiscount.DiscountDurationTooShort);

        if (discountUnit == DiscountUnit.Percentage && discountValue >= 80) return Result.Fail(DomainErrors.ProductDiscount.DiscountPercentageExceedsLimit);

        if (discountUnit == DiscountUnit.FixedAmount)
        {
            decimal maxDiscountAmount = productPrice * 0.8m;
            if (discountValue >= maxDiscountAmount) return Result.Fail(DomainErrors.ProductDiscount.MaximumFixedDiscountExceeded);
        }

        if (maximumDiscountAmount is not null &&
            discountUnit == DiscountUnit.FixedAmount &&
            maximumDiscountAmount >= discountValue
        )
        {
            return Result.Fail(DomainErrors.ProductDiscount.MaximumDiscountAmountExceedsValue);
        }

        return Result.Ok();
    }
}
