namespace Ecommerce.Common.Infra.Representation.Grid;

/// <summary>
/// Represents a sort parameter for sorting data in the grid.
/// </summary>
public record SortParams
{
    /// <summary>
    /// Gets or sets the property name to sort on.
    /// </summary>
    public string Property { get; set; } = "";

    /// <summary>
    /// Gets or sets the sort direction ("asc" for ascending, "desc" for descending).
    /// </summary>
    public string Direction { get; set; } = "asc";
}
