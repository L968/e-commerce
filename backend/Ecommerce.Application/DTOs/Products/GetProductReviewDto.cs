namespace Ecommerce.Application.DTOs.Products;

public record GetProductReviewDto
{
    public int Rating { get; init; }
    public string? Description { get; init; }
    public DateTime CreatedAt { get; init; }
}
