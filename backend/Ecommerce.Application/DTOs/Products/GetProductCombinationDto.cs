namespace Ecommerce.Application.DTOs.Products;

public record GetProductCombinationDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string Name { get; init; } = "";
    public string CombinationString { get; init; } = "";
    public decimal OriginalPrice { get; init; }
    public decimal DiscountedPrice { get; init; }
    public int Stock { get; init; }
    public float Length { get; init; }
    public float Width { get; init; }
    public float Height { get; init; }
    public float Weight { get; init; }

    public List<string> Images { get; init; } = [];
}
