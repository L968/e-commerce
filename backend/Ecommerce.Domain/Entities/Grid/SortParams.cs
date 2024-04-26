namespace Ecommerce.Domain.Entities.Grid;

public record SortParams
{
    public string Property { get; set; } = "";
    public string Direction { get; set; } = "asc";
}
