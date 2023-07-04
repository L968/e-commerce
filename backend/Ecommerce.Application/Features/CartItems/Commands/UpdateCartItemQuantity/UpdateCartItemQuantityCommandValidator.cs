namespace Ecommerce.Application.Features.CartItems.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandValidator : AbstractValidator<UpdateCartItemQuantityCommand>
{
    public UpdateCartItemQuantityCommandValidator()
    {
        RuleFor(cartItem => cartItem.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}