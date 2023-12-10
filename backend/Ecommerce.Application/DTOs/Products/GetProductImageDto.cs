namespace Ecommerce.Application.DTOs.Products;

public record GetProductImageDto
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
}
