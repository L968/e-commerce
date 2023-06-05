namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public int ProductVariantId { get; private set; }
    public int Quantity { get; private set; }

    public Cart? Cart { get; set; }
    public ProductVariant? ProductVariant { get; set; }

    public CartItem(int cartId, int productVariantId, int quantity)
    {
        ValidateCartId(cartId);
        ValidateProductVariantId(productVariantId);
        ValidateQuantity(quantity);

        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public CartItem(int id, int cartId, int productVariantId, int quantity)
    {
        ValidateId(id);
        ValidateCartId(cartId);
        ValidateProductVariantId(productVariantId);
        ValidateQuantity(quantity);

        Id = id;
        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public void Update(int quantity)
    {
        ValidateQuantity(quantity);

        Quantity = quantity;
    }

    private void ValidateId(int id)
    {
        DomainExceptionValidation.When(id <= 0,
            $"Invalid {nameof(id)} value");
    }

    private void ValidateCartId(int cartId)
    {
        DomainExceptionValidation.When(cartId <= 0,
            $"Invalid {nameof(cartId)} value");
    }

    private void ValidateProductVariantId(int productVariantId)
    {
        DomainExceptionValidation.When(productVariantId <= 0,
            $"Invalid {nameof(productVariantId)} value");
    }

    private void ValidateQuantity(int quantity)
    {
        DomainExceptionValidation.When(quantity <= 0,
            $"Invalid {nameof(quantity)} value");
    }
}