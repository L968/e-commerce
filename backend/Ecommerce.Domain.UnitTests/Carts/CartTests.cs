using Ecommerce.Domain.Entities.CartEntities;
using Ecommerce.Domain.Errors;

namespace Ecommerce.Domain.UnitTests.Carts;

public class CartTests
{
    [Fact]
    public void GivenValidUserId_CartShouldBeCreated()
    {
        // Arrange
        var userId = 1;

        // Act
        var cart = new Cart(userId);

        // Assert
        Assert.NotEqual(Guid.Empty, cart.Id);
        Assert.Equal(userId, cart.UserId);
    }

    [Fact]
    public void GivenCart_AddCartItemShouldNotReturnErrors()
    {
        // Arrange
        var userId = 1;
        var cart = new Cart(userId);
        var cartItem = new CartItem(cart.Id, Guid.NewGuid(), 1);

        // Act
        cart.AddCartItem(cartItem);

        // Assert
        Assert.Single(cart.CartItems);
    }

    [Fact]
    public void GivenCartItemNotBelongsToCart_AddCartItemShouldReturnError()
    {
        // Arrange
        var userId = 1;
        var cart = new Cart(userId);
        var cartItem = new CartItem(Guid.NewGuid(), Guid.NewGuid(), 1);

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => cart.AddCartItem(cartItem));
    }

    [Fact]
    public void GivenExistingCartItem_RemoveCartItemShouldRemoveCartItem()
    {
        // Arrange
        var cart = new Cart(1);
        var cartItem = new CartItem(cart.Id, Guid.NewGuid(), 1);
        cart.AddCartItem(cartItem);

        // Act
        cart.RemoveCartItem(cartItem.Id);

        // Assert
        Assert.DoesNotContain(cartItem, cart.CartItems);
    }

    [Fact]
    public void ClearItems_ShouldClearAllCartItems()
    {
        // Arrange
        var cart = new Cart(1);
        var cartItems = new List<CartItem>
        {
            new CartItem(cart.Id, Guid.NewGuid(), 1),
            new CartItem(cart.Id, Guid.NewGuid(), 1),
            new CartItem(cart.Id, Guid.NewGuid(), 1)
        };

        foreach (var cartItem in cartItems)
        {
            cart.AddCartItem(cartItem);
        }

        // Act
        cart.ClearItems();

        // Assert
        Assert.Empty(cart.CartItems);
    }
}
