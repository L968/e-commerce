using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Errors;

namespace Ecommerce.Domain.UnitTests.Products
{
    public class ProductDiscountTests
    {
        [Fact]
        public void GivenStartDateInPast_ShouldFail()
        {
            // Arrange and Act
            var exception = Assert.Throws<DomainException>(() => new ProductDiscount(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.UtcNow.AddDays(-1),
                validUntil: DateTime.UtcNow.AddDays(2),
                productPrice: 100
            ));

            // Assert
            Assert.Contains(DomainErrors.ProductDiscount.DiscountStartDateInPast, exception.Errors);
        }

        [Fact]
        public void GivenEndDateGreaterThanStartDate_ShouldFail()
        {
            // Arrange and Act
            var exception = Assert.Throws<DomainException>(() => new ProductDiscount(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.UtcNow.AddDays(2),
                validUntil: DateTime.UtcNow.AddDays(1),
                productPrice: 100
            ));

            // Assert
            Assert.Contains(DomainErrors.ProductDiscount.DiscountEndDateMustBeAfterStartDate, exception.Errors);
        }

        [Fact]
        public void GivenShortDiscountPeriod_ShouldFail()
        {
            // Arrange and Act
            var exception = Assert.Throws<DomainException>(() => new ProductDiscount(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.Today.AddDays(1).Date.AddHours(12),
                validUntil: DateTime.Today.AddDays(1).Date.AddHours(12).AddMinutes(1),
                productPrice: 100
            ));

            // Assert
            Assert.Contains(DomainErrors.ProductDiscount.DiscountDurationTooShort, exception.Errors);
        }

        [Fact]
        public void GivenMaximumDiscountAmountExceedsValue_ShouldFail()
        {
            // Arrange and Act
            var exception = Assert.Throws<DomainException>(() => new ProductDiscount(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 50,
                discountUnit: DiscountUnit.FixedAmount,
                maximumDiscountAmount: 60,
                validFrom: DateTime.UtcNow.AddDays(1),
                validUntil: DateTime.UtcNow.AddDays(2),
                productPrice: 100
            ));

            // Assert
            Assert.Contains(DomainErrors.ProductDiscount.MaximumDiscountAmountExceedsValue, exception.Errors);
        }

        [Fact]
        public void GivenValidData_ShouldCreateProductDiscount()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var name = "Summer Sale";
            var discountValue = 20;
            var discountUnit = DiscountUnit.Percentage;
            var maximumDiscountAmount = (decimal?)null;
            var validFrom = DateTime.UtcNow.AddDays(1);
            var validUntil = DateTime.UtcNow.AddDays(2);
            var productPrice = 100;

            // Act
            var result = new ProductDiscount(
                productId: productId,
                name: name,
                discountValue: discountValue,
                discountUnit: discountUnit,
                maximumDiscountAmount: maximumDiscountAmount,
                validFrom: validFrom,
                validUntil: validUntil,
                productPrice: productPrice
            );

            // Assert
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(name, result.Name);
            Assert.Equal(discountValue, result.DiscountValue);
            Assert.Equal(discountUnit, result.DiscountUnit);
            Assert.Equal(maximumDiscountAmount, result.MaximumDiscountAmount);
            Assert.Equal(validFrom, result.ValidFrom);
            Assert.Equal(validUntil, result.ValidUntil);
        }
    }
}
