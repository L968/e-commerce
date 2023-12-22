namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.Active).NotEmpty();
        RuleFor(p => p.Visible).NotEmpty();
        RuleFor(p => p.ProductCategoryId).NotEmpty();
    }
}
