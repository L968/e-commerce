namespace Ecommerce.Application.DTOs.Products.Admin;

public record GetProductCombinationAdminDto
{
    public Guid Id { get; init; }
    public string CombinationString { get; init; } = "";
    public string Sku { get; init; } = "";
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public float Length { get; init; }
    public float Width { get; init; }
    public float Height { get; init; }
    public float Weight { get; init; }

    public List<string> Images { get; init; } = [];
}
