namespace Ecommerce.Application.Products.Queries;

public record GetProductImageDto
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
}