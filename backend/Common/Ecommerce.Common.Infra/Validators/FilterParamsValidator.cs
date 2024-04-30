using Ecommerce.Common.Infra.Representation.Grid;
using FluentValidation;

namespace Ecommerce.Common.Infra.Validators;

public class FilterParamsValidator : AbstractValidator<FilterParams>
{
    private static readonly string[] ValidOperators = ["=", "like", ">", ">=", "<", "<="];

    public FilterParamsValidator()
    {
        RuleFor(x => x.Operator)
            .Must(x => ValidOperators.Contains(x))
            .WithMessage($"Filter operator must be one of: {string.Join(", ", ValidOperators)}");
    }
}
