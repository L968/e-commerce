namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem : AuditableEntity
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public bool IsSelectedForCheckout { get; private set; }
    public Product Product { get; set; }

    private CartItem(int cartId, Guid productId, int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(int cartId, Guid productId, int quantity)
    {
        if (quantity <= 0) return Result.Fail(DomainErrors.CartItem.InvalidQuantity);

        return Result.Ok(new CartItem(cartId, productId, quantity));
    }

    public void IncrementQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void SetIsSelectedForCheckout(bool isSelectedForCheckout)
    {
        IsSelectedForCheckout = isSelectedForCheckout;
    }
}