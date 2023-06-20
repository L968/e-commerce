namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class CartItem : AuditableEntity
{
    public int Id { get; private set; }
    public int CartId { get; private set; }
    public int ProductVariantId { get; private set; }

    private int _quantity;
    public int Quantity {
        get => _quantity;
        private set
        {
            DomainExceptionValidation.When(value <= 0,
                "Quantity must be greater than 0");

            _quantity = value;
        }
    }

    public ProductVariant? ProductVariant { get; set; }

    public CartItem(int cartId, int productVariantId, int quantity)
    {
        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public CartItem(int id, int cartId, int productVariantId, int quantity)
    {
        Id = id;
        CartId = cartId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
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