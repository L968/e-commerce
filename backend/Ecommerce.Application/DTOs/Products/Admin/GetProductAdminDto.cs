namespace Ecommerce.Application.DTOs.Products.Admin;

public record GetProductAdminDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public bool Active { get; init; }
    public bool Visible { get; init; }

    public GetProductCategoryDto? Category { get; init; }
    public List<GetProductCombinationAdminDto> Combinations { get; init; } = [];
}
