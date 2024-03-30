namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public List<GetVariantOptionDto> Options { get; set; } = [];
}
