using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.DTOs.Carts;

public record GetCartItemDto
{
    public int Id { get; init; }
    public int Quantity { get; init; }
    public GetProductCombinationDto? Product { get; init; }
}
