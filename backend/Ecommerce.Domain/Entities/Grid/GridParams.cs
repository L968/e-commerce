namespace Ecommerce.Domain.Entities.Grid;

public record GridParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public IList<FilterParams> Filters { get; set; } = [];
    public IList<SortParams> Sorters { get; set; } = [];
}
