namespace Ecommerce.Application.CartItems.Commands.CreateCartItem;

public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
{
    public CreateCartItemCommandValidator()
    {
        RuleFor(cartItem => cartItem.ProductVariantId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(cartItem => cartItem.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}