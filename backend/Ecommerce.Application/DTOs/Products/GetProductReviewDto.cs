namespace Ecommerce.Application.DTOs.Products;

public record GetProductReviewDto
{
    public int Rating { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; set; }
}
