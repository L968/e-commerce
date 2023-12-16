namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<GetVariantOptionDto> Options { get; set; } = new();
}
