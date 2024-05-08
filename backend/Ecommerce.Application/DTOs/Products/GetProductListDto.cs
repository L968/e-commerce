using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.DTOs.Products;

public record GetProductListDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string CategoryName { get; init; } = "";
    public decimal? OriginalPrice { get; init; }
    public decimal? DiscountedPrice { get; init; }
    public string? ImageSource { get; init; } = "";
    public float Rating { get; init; }
    public int ReviewsCount { get; init; }

    public List<GetVariantDto> Variants { get; init; } = [];
}
