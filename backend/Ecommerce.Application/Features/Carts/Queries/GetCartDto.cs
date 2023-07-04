using Ecommerce.Application.Features.CartItems.Queries;

namespace Ecommerce.Application.Features.Carts.Queries;

public record GetCartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<GetCartItemDto> CartItems { get; set; } = null!;
}