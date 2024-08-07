﻿namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class Cart
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }

    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems;

    public Cart(int userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
    }

    public void AddCartItem(CartItem cartItem)
    {
        if (cartItem.CartId != Id)
            throw new DomainException(DomainErrors.Cart.CartItemNotBelongsToCart);

        CartItem? existingCartItem = _cartItems.FirstOrDefault(ci => ci.ProductCombinationId == cartItem.ProductCombinationId);

        if (existingCartItem is not null)
        {
            existingCartItem.IncrementQuantity(cartItem.Quantity);
        }
        else
        {
            _cartItems.Add(cartItem);
        }
    }

    public void RemoveCartItem(int cartItemId)
    {
        CartItem? cartItem = _cartItems.FirstOrDefault(item => item.Id == cartItemId);

        if (cartItem is not null)
        {
            _cartItems.Remove(cartItem);
        }
    }

    public void ClearItems()
    {
        _cartItems.Clear();
    }

    public void ClearItems(Guid[] productCombinationIds)
    {
        foreach (Guid id in productCombinationIds)
        {
            var cartItemToRemove = _cartItems.FirstOrDefault(ci => ci.ProductCombinationId == id);

            if (cartItemToRemove is not null)
            {
                _cartItems.Remove(cartItemToRemove);
            }
        }
    }
}
