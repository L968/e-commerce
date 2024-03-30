namespace Ecommerce.Application.DTOs.Carts;

public record GetCartDto
{
    public List<GetCartItemDto> CartItems { get; set; } = [];
}
