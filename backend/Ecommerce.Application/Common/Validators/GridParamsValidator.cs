using Ecommerce.Domain.Entities.Grid;

namespace Ecommerce.Application.Common.Validators;

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
