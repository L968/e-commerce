using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.DTOs.Products;

public record GetProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Active { get; set; }
    public float Rating { get; set; }

    public List<GetProductCombinationDto> Combinations { get; set; } = [];
    public List<GetVariantDto> Variants { get; set; } = [];
    public List<GetProductReviewDto> Reviews { get; set; } = [];
}
