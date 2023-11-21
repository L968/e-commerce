using Ecommerce.Application.Features.ProductCategories.Queries;

namespace Ecommerce.Application.Features.Products.Queries;

public record GetProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Active { get; set; }
    public bool Visible { get; set; }

    public GetProductCategoryDto? Category { get; set; }
}
