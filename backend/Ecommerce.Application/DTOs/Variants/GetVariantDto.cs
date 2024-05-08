namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public List<GetVariantOptionDto> Options { get; init; } = [];
}
