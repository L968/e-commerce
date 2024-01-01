namespace Ecommerce.Application.DTOs.Carts;

public record GetCartDto
{
    public IEnumerable<GetCartItemDto> CartItems { get; set; } = null!;
}
