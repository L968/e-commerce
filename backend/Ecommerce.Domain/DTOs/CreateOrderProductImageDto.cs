namespace Ecommerce.Domain.DTOs;

public record CreateOrderProductImageDto
{
    public int Id { get; init; }
    public string? ImagePath { get; init; }
}
