namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem : AuditableEntity
{
    public int Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public int Quantity { get; private set; }

    public Cart? Cart { get; private set; }
    public ProductCombination? ProductCombination { get; private set; } = null!;

    private CartItem() { }

    private CartItem(Guid cartId, Guid productCombinationId, int quantity)
    {
        CartId = cartId;
        ProductCombinationId = productCombinationId;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(Guid cartId, Guid productCombinationId, int quantity)
    {
        if (quantity <= 0)
            return DomainErrors.CartItem.InvalidQuantity;

        return Result.Ok(new CartItem(
            cartId,
            productCombinationId,
            quantity
        ));
    }

    public void IncrementQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
