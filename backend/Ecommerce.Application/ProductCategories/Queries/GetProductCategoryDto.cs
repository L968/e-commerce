namespace Ecommerce.Application.ProductCategories.Queries;

public record GetProductCategoryDto
{
    public Guid? Guid { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}