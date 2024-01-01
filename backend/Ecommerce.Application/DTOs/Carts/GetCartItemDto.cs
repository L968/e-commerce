using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.DTOs.Carts;

public record GetCartItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public GetProductCombinationDto? Product { get; set; }
}
