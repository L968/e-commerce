namespace Ecommerce.Application.DTOs.Products;

public record GetProductImageDto
{
    public int Id { get; init; }
    public string? ImagePath { get; init; }
}
