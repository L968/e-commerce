namespace Ecommerce.Domain.DTOs;

public record CreateOrderProductCombinationDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string CombinationString { get; init; } = "";
    public string Sku { get; init; } = "";
    public decimal Price { get; init; }
    public float Length { get; init; }
    public float Width { get; init; }
    public float Height { get; init; }
    public float Weight { get; init; }

    public CreateOrderProductDto Product { get; init; }
    public CreateOrderProductInventoryDto Inventory { get; init; }
    public List<CreateOrderProductImageDto> Images { get; init; }
}
