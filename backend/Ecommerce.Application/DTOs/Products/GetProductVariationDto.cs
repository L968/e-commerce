namespace Ecommerce.Application.DTOs.Products;

public record GetProductVariationDto
{
    public string Name { get; set; }
    public List<string> Options { get; set; } = new();
}
