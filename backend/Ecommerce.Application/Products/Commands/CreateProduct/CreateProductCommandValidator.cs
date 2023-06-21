namespace Ecommerce.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Sku).NotEmpty();
        RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Active).NotEmpty();
        RuleFor(p => p.Visible).NotEmpty();
        RuleFor(p => p.Length).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Width).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Height).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Weight).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Stock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(p => p.ProductCategoryGuid).NotEmpty();
    }
}