namespace Ecommerce.Application.Features.Variants.Commands.UpdateVariant;

public class UpdateVariantCommandValidator : AbstractValidator<UpdateVariantCommand>
{
    public UpdateVariantCommandValidator()
    {
        RuleFor(pc => pc.Name)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(pc => pc.Options)
            .NotEmpty();
    }
}
