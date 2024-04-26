using Ecommerce.Domain.Entities.Grid;

namespace Ecommerce.Application.Common.Validators;

public class SorterParamsValidator : AbstractValidator<SortParams>
{
    private static readonly string[] ValidDirections = ["asc", "desc"];

    public SorterParamsValidator()
    {
        RuleFor(x => x.Direction)
            .Must(x => ValidDirections.Contains(x))
            .WithMessage($"Sorter direction must be one of: {string.Join(", ", ValidDirections)}");
    }
}
