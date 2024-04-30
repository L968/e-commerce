using Ecommerce.Common.Infra.Representation.Grid;
using FluentValidation;

namespace Ecommerce.Common.Infra.Validators;

public class GridParamsValidator : AbstractValidator<GridParams>
{
    public GridParamsValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleForEach(x => x.Filters).SetValidator(new FilterParamsValidator());
        RuleForEach(x => x.Sorters).SetValidator(new SorterParamsValidator());
    }
}
