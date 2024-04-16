namespace Ecommerce.Domain.DTOs;

public record CreateOrderProductInventoryDto
{
    public int Id { get; init; }
    public Guid ProductCombinationId { get; init; }
    public int Stock { get; init; }
}
