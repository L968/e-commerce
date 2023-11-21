using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.UnitTests
{
    public class ProductDiscountTests
    {
        [Fact]
        public void GivenStartDateInPast_ShouldReturnFailedResult()
        {
            // Arrange and Act
            var result = ProductDiscount.Create(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.UtcNow.AddDays(-1),
                validUntil: DateTime.UtcNow.AddDays(2),
                productPrice: 100
            );

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Discount start date cannot be in the past", result.Reasons[0].Message);
        }

        [Fact]
        public void GivenEndDateGreaterThanStartDate_ShouldReturnFailedResult()
        {
            // Arrange and Act
            var result = ProductDiscount.Create(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.UtcNow.AddDays(2),
                validUntil: DateTime.UtcNow.AddDays(1),
                productPrice: 100
            );

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Discount end date must be later than start date", result.Reasons[0].Message);
        }

        [Fact]
        public void GivenShortDiscountPeriod_ShouldReturnFailedResult()
        {
            // Arrange and Act
            var result = ProductDiscount.Create(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.Today.AddDays(1).Date.AddHours(12),
                validUntil: DateTime.Today.AddDays(1).Date.AddHours(12).AddMinutes(1),
                productPrice: 100
            );

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Discount duration must be at least 5 minutes", result.Reasons[0].Message);
        }

        [Fact]
        public void GivenMaximumDiscountAmountExceedsValue_ShouldReturnFailedResult()
        {
            // Arrange and Act
            var result = ProductDiscount.Create(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 50,
                discountUnit: DiscountUnit.FixedAmount,
                maximumDiscountAmount: 60,
                validFrom: DateTime.UtcNow.AddDays(1),
                validUntil: DateTime.UtcNow.AddDays(2),
                productPrice: 100
            );

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Maximum discount amount cannot be greater than or equal to the discount value", result.Reasons[0].Message);
        }

        [Fact]
        public void GivenValidData_ShouldCreateProductDiscount()
        {
            // Arrange and Act
            var result = ProductDiscount.Create(
                productId: Guid.NewGuid(),
                name: "Summer Sale",
                discountValue: 20,
                discountUnit: DiscountUnit.Percentage,
                maximumDiscountAmount: null,
                validFrom: DateTime.UtcNow.AddDays(1),
                validUntil: DateTime.UtcNow.AddDays(2),
                productPrice: 100
            );

            // Assert
            Assert.False(result.IsFailed);
            Assert.NotNull(result.Value);
        }
    }
}
