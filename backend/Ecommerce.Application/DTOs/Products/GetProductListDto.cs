namespace Ecommerce.Application.DTOs.Products;

public record GetProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string ImageSource { get; set; }
    public float Rating { get; private set; }
    public int ReviewsCount { get; set; }

    public List<GetProductVariationDto> Variations { get; private set; } = new();
}
