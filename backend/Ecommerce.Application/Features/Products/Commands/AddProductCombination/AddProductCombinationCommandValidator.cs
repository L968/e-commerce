namespace Ecommerce.Application.Features.Products.Commands.AddProductCombination;

public class AddProductCombinationCommandValidator : AbstractValidator<AddProductCombinationCommand>
{
    public AddProductCombinationCommandValidator()
    {
        RuleFor(p => p.VariantOptionIds).NotEmpty();
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
