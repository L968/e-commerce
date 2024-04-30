namespace Ecommerce.Common.Infra.Representation.Grid;

/// <summary>
/// Represents a filter parameter for filtering data in the grid.
/// </summary>
public record FilterParams
{
    /// <summary>
    /// Gets or sets the property name to filter on.
    /// </summary>
    public string Property { get; set; } = "";

    /// <summary>
    /// Gets or sets the operator for the filter (e.g., "=", ">", "<", etc.).
    /// </summary>
    public string Operator { get; set; } = "=";

    /// <summary>
    /// Gets or sets the value to filter on.
    /// </summary>
    public string Value { get; set; } = "";
}
