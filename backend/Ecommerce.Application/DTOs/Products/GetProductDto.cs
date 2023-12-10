namespace Ecommerce.Application.DTOs.Products;

public record GetProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public float Rating { get; private set; }

    public List<GetProductCombinationDto> Combinations { get; private set; } = new();
    public List<GetProductVariationDto> Variations { get; private set; } = new();
    public List<GetProductReviewDto> Reviews { get; private set; } = new();
}
