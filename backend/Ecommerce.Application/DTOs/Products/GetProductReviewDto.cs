namespace Ecommerce.Application.DTOs.Products;

public record GetProductReviewDto
{
    public string UserName { get; private set; }
    public int Rating { get; private set; }
    public string? Description { get; private set; }
}
