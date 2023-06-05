namespace Ecommerce.Domain.Entities.CartEntities;

public sealed class Cart
{
    public int Id { get; private set; }
    public int UserId { get; private set; }

    public List<CartItem>? CartItems { get; set; }

    public Cart(int userId)
    {
        ValidateUserId(userId);

        UserId = userId;
    }

    public Cart(int id, int userId)
    {
        ValidateId(id);
        ValidateUserId(userId);

        Id = id;
        UserId = userId;
    }

    private void ValidateId(int id)
    {
        DomainExceptionValidation.When(id <= 0,
            $"Invalid {nameof(id)} value");
    }

    private void ValidateUserId(int userId)
    {
        DomainExceptionValidation.When(userId <= 0,
            $"Invalid {nameof(userId)} value");
    }
}