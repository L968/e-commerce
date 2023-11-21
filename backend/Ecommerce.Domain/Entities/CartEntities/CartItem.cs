namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem : AuditableEntity
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Quantity { get; private set; }
    public bool IsSelectedForCheckout { get; private set; }

    public ProductCombination? ProductCombination { get; set; }

    private CartItem() { }

    private CartItem(int cartId, Guid productCombinationId, int quantity, bool isSelectedForCheckout)
    {
        CartId = cartId;
        ProductCombinationId = productCombinationId;
        Quantity = quantity;
        IsSelectedForCheckout = isSelectedForCheckout;
    }

    public static Result<CartItem> Create(int cartId, Guid productCombinationId, int quantity, bool isSelectedForCheckout)
    {
        if (quantity <= 0) return Result.Fail(DomainErrors.CartItem.InvalidQuantity);

        return Result.Ok(new CartItem(cartId, productCombinationId, quantity, isSelectedForCheckout));
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
