namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem : AuditableEntity
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public int ProductVariantId { get; private set; }
    public int Quantity { get; private set; }
    public ProductVariant? ProductVariant { get; set; }

    private CartItem(int cartId, int productVariantId, int quantity)
    {
        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(int cartId, int productVariantId, int quantity)
    {
        if (quantity <= 0) return Result.Fail(DomainErrors.CartItem.InvalidQuantity);

        return Result.Ok(new CartItem(cartId, productVariantId, quantity));
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