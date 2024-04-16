namespace Ecommerce.Application.DTOs.Products.Admin;

public record GetProductCombinationAdminDto
{
    public Guid Id { get; init; }
    public string CombinationString { get; set; } = "";
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public float Length { get; set; }
    public float Width { get; private set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    public List<string> Images { get; set; } = [];
}
