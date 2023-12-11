namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantDto
{
    public int Id { get; private set; }
    public string Name { get; private set; } = "";
    public List<GetVariantOptionDto> Options { get; private set; } = new();
}
