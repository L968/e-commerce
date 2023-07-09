namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class Cart
{
    public int Id { get; private set; }
    public int UserId { get; private set; }

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems;

    public Cart(int userId)
    {
        UserId = userId;
    }

    public Result AddCartItem(CartItem cartItem)
    {
        if (cartItem.CartId != Id) return Result.Fail(DomainErrors.Cart.CartItemNotBelongsToCart);

        CartItem? existingCartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);

        if (existingCartItem is not null)
        {
            existingCartItem.IncrementQuantity(cartItem.Quantity);
        }
        else
        {
            _cartItems.Add(cartItem);
        }

        return Result.Ok();
    }

    public void RemoveCartItem(int cartItemId)
    {
        CartItem? cartItem = _cartItems.FirstOrDefault(item => item.Id == cartItemId);

        if (cartItem is not null)
        {
            _cartItems.Remove(cartItem);
        }
    }
}