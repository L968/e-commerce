namespace Ecommerce.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.ProductCategoryId).NotEmpty();
        RuleFor(p => p.Active).NotEmpty();
        RuleFor(p => p.Visible).NotEmpty();
    }
}
