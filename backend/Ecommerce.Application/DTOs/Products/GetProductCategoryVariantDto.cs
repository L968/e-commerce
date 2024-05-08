namespace Ecommerce.Application.DTOs.Products;

public record GetProductCategoryVariantDto
{
    public string Name { get; init; } = "";
    public List<string> Options { get; init; } = [];
}
