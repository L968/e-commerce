namespace Ecommerce.Domain.DTOs;

public record CreateOrderProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public bool Active { get; init; }
    public bool Visible { get; init; }
    public Guid ProductCategoryId { get; init; }

    public List<CreateOrderProductDiscountDto> Discounts { get; init; } = [];
}
