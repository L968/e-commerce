using Ecommerce.Domain.Entities.CartEntities;

namespace Ecommerce.Domain.UnitTests.Carts;

public class CartItemTests
{
    [Fact]
    public void Create_ValidData_ShouldCreateCartItem()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var productCombinationId = Guid.NewGuid();
        var quantity = 1;

        // Act
        var result = CartItem.Create(cartId, productCombinationId, quantity);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_InvalidQuantity_ShouldFail(int quantity)
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var productCombinationId = Guid.NewGuid();

        // Act
        var result = CartItem.Create(cartId, productCombinationId, quantity);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void IncrementQuantity_ValidQuantity_ShouldIncreaseQuantity()
    {
        // Arrange
        var cartItem = CartItem.Create(Guid.NewGuid(), Guid.NewGuid(), 1).Value;
        var quantity = 2;

        // Act
        cartItem.IncrementQuantity(quantity);

        // Assert
        Assert.Equal(3, cartItem.Quantity);
    }

    [Fact]
    public void SetQuantity_ValidQuantity_ShouldSetQuantity()
    {
        // Arrange
        var cartItem = CartItem.Create(Guid.NewGuid(), Guid.NewGuid(), 1).Value;
        var quantity = 5;

        // Act
        cartItem.SetQuantity(quantity);

        // Assert
        Assert.Equal(quantity, cartItem.Quantity);
    }
}
