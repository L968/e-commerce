namespace Ecommerce.Application.DTOs.Products;

public record GetProductCombinationByIdDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string Name { get; init; } = "";
    public string CombinationString { get; init; } = "";
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public float Length { get; init; }
    public float Width { get; init; }
    public float Height { get; init; }
    public float Weight { get; init; }

    public GetProductByIdDto Product { get; init; }
    public GetProductInventoryDto Inventory { get; init; }
    public List<GetProductImageDto> Images { get; init; } = [];
}
