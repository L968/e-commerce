namespace Ecommerce.Application.DTOs.Variants;

public record GetVariantOptionDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
