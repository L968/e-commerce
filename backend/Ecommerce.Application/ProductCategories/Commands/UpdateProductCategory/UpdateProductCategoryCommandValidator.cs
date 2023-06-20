using Ecommerce.Application.ProductCategories.Commands.CreateProductCategory;

namespace Ecommerce.Application.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public UpdateProductCategoryCommandValidator()
    {
        RuleFor(pc => pc.Name)
            .NotEmpty()
            .MinimumLength(2);
    }
}