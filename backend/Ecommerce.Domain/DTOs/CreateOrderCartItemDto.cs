namespace Ecommerce.Domain.DTOs;

public record CreateOrderCartItemDto
{
    public ProductCombination ProductCombination { get; init; }
    public int Quantity { get; init; }
}
