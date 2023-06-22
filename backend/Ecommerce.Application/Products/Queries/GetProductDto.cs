using Ecommerce.Application.ProductCategories.Queries;

namespace Ecommerce.Application.Products.Queries;

public record GetProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public bool Visible { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public List<GetProductImageDto>? Images { get; set; }
    public GetProductCategoryDto? Category { get; set; }
}