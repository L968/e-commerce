namespace Ecommerce.Common.Infra.Representation.Grid;

/// <summary>
/// Represents the grid parameters for filtering, sorting, and pagination.
/// </summary>
public record GridParams
{
    /// <summary>
    /// Gets or sets the page number (1-based) for pagination.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for pagination.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the list of filter parameters for filtering the data.
    /// </summary>
    public IList<FilterParams> Filters { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of sort parameters for sorting the data.
    /// </summary>
    public IList<SortParams> Sorters { get; set; } = [];
}
