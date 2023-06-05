namespace Ecommerce.Application.DTO.ProductDto;

public record CreateProductCategoryDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    public string? Description { get; init; }
}