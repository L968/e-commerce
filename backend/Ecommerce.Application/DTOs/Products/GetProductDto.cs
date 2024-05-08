using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.DTOs.Products;

public record GetProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public bool Active { get; init; }
    public float Rating { get; init; }

    public List<GetProductCombinationDto> Combinations { get; init; } = [];
    public List<GetVariantDto> Variants { get; init; } = [];
    public List<GetProductReviewDto> Reviews { get; init; } = [];
}
