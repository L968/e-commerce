namespace Ecommerce.Application.DTOs.Products;

public record GetProductAdminDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Active { get; set; }
    public bool Visible { get; set; }

    public GetProductCategoryDto? Category { get; set; }
    public List<GetProductCombinationAdminDto> Combinations { get; set; } = new();
}
