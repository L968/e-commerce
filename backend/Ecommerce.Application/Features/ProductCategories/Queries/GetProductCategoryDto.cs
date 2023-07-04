namespace Ecommerce.Application.Features.ProductCategories.Queries;

public record GetProductCategoryDto
{
    public Guid? Guid { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}