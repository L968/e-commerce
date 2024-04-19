using Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.ReduceStockProductCombination;

public class ReduceStockProductCombinationCommandValidator : AbstractValidator<ReduceStockProductCombinationCommand>
{
    public ReduceStockProductCombinationCommandValidator()
    {
        RuleForEach(p => p.Requests)
            .ChildRules(request =>
            {
                request.RuleFor(r => r.ProductCombinationId).NotEmpty();
                request.RuleFor(r => r.Quantity).NotEmpty().GreaterThan(0);
            });
    }
}
