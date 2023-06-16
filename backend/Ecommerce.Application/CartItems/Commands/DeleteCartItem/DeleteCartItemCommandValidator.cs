namespace Ecommerce.Application.CartItems.Commands.DeleteCartItem;

public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
{
    public DeleteCartItemCommandValidator()
    {
        RuleFor(ci => ci.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}