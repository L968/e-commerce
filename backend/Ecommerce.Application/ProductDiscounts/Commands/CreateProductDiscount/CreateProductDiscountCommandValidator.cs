namespace Ecommerce.Application.ProductDiscounts.Commands.CreateProductDiscount;

public class CreateProductDiscountCommandValidator : AbstractValidator<CreateProductDiscountCommand>
{
    public CreateProductDiscountCommandValidator()
    {
        RuleFor(pd => pd.ProductId).NotEmpty();
        RuleFor(pd => pd.Name).NotEmpty();
        RuleFor(pd => pd.DiscountValue).GreaterThan(0);
        RuleFor(pd => pd.DiscountUnit).IsInEnum();
        RuleFor(pd => pd.ValidFrom).NotEmpty();
        RuleFor(pd => pd.ValidUntil).GreaterThan(pd => pd.ValidFrom).WithMessage("ValidUntil must be greater than ValidFrom");
    }
}