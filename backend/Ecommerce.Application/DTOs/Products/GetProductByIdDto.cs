namespace Ecommerce.Application.DTOs.Products;

public record GetProductByIdDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Active { get; set; }
    public float Rating { get; set; }

    public List<GetProductDiscountDto> Discounts { get; set; } = [];
}
