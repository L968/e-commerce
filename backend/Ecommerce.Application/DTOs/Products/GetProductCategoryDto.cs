namespace Ecommerce.Application.DTOs.Products;

public record GetProductCategoryDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
