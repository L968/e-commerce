namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantDto
{
    public string Name { get; set; } = "";
    public List<GetVariantOptionDto> Options { get; set; } = new();
}
