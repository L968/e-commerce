using Ecommerce.Domain.Entities.ProductEntities;

namespace Ecommerce.Domain.UnitTests;

public class ProductTests
{
    [Fact]
    public void GivenValidData_ShouldCreateProductDiscount()
    {
        // Arrange and Act
        var result = Product.Create(
            name: "T-Shirt",
            description: "Brand new T-Shirt",
            active: true,
            visible: true,
            productCategoryId: 1
        );

        // Assert
        Assert.False(result.IsFailed);
        Assert.NotNull(result.Value);
    }
}
