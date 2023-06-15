using Ecommerce.Application.CartItems.Queries;

namespace Ecommerce.Application.Carts.Queries;

public record GetCartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<GetCartItemDto> CartItems { get; set; } = null!;
}