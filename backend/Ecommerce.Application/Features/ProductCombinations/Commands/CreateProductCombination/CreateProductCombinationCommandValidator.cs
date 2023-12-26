namespace Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;

public class CreateProductCombinationCommandValidator : AbstractValidator<CreateProductCombinationCommand>
{
    public CreateProductCombinationCommandValidator()
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
