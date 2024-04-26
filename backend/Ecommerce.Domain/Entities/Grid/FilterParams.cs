namespace Ecommerce.Domain.Entities.Grid;

public record FilterParams
{
    public string Property { get; set; } = "";
    public string Operator { get; set; } = "=";
    public string Value { get; set; } = "";
}
