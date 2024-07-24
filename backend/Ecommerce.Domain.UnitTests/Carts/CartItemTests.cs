using Ecommerce.Domain.Entities.CartEntities;
using Ecommerce.Domain.Errors;

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
        var cartItem = new CartItem(cartId, productCombinationId, quantity);

        // Assert
        Assert.Equal(cartId, cartItem.CartId);
        Assert.Equal(productCombinationId, cartItem.ProductCombinationId);
        Assert.Equal(quantity, cartItem.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_InvalidQuantity_ShouldFail(int quantity)
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var productCombinationId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new CartItem(cartId, productCombinationId, quantity));
    }

    [Fact]
    public void IncrementQuantity_ValidQuantity_ShouldIncreaseQuantity()
    {
        // Arrange
        var cartItem = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 1);
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
        var cartItem = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 1);
        var quantity = 5;

        // Act
        cartItem.SetQuantity(quantity);

        // Assert
        Assert.Equal(quantity, cartItem.Quantity);
    }
}
