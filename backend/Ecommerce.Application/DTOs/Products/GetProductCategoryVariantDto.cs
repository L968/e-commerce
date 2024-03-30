namespace Ecommerce.Application.DTOs.Products;

public record GetProductCategoryVariantDto
{
    public string Name { get; set; } = "";
    public List<string> Options { get; set; } = [];
}
