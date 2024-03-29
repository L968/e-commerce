﻿namespace Ecommerce.Application.Features.CartItems.Commands.CreateCartItem;

public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
{
    public CreateCartItemCommandValidator()
    {
        RuleFor(cartItem => cartItem.ProductCombinationId)
            .NotEmpty();

        RuleFor(cartItem => cartItem.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}