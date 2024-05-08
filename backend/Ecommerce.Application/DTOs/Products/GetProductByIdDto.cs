namespace Ecommerce.Application.DTOs.Products;

public record GetProductByIdDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public bool Active { get; init; }
    public float Rating { get; init; }

    public List<GetProductDiscountDto> Discounts { get; init; } = [];
}
