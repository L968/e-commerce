namespace Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(pc => pc.Name)
            .NotEmpty()
            .MinimumLength(2);
    }
}