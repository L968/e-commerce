namespace Ecommerce.Application.Features.ProductCombinations.Commands.UpdateProductCombination;

public class UpdateProductCombinationCommandValidator : AbstractValidator<UpdateProductCombinationCommand>
{
    public UpdateProductCombinationCommandValidator()
    {
        RuleFor(p => p.Sku).NotEmpty();
        RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Stock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(p => p.Images).NotEmpty();
        RuleFor(p => p.Length).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Width).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Height).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Weight).NotEmpty().GreaterThan(0);
    }
}
