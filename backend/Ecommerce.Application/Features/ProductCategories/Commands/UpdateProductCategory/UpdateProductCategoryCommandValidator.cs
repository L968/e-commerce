namespace Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommandValidator()
    {
        RuleFor(pc => pc.Name)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(pc => pc.VariantIds)
            .NotEmpty();
    }
}
