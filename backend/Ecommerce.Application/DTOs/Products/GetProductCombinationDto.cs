namespace Ecommerce.Application.DTOs.Products;

public record GetProductCombinationDto
{
    public Guid Id { get; private init; }
    public string CombinationString { get; private set; } = "";
    public decimal OriginalPrice { get; private set; }
    public decimal DiscountedPrice { get; private set; }
    public int Stock { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }

    public List<string> Images { get; private set; } = new();
}
