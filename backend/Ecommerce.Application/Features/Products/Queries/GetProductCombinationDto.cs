namespace Ecommerce.Application.Features.Products.Queries;

public class GetProductCombinationDto
{
    public Guid Id { get; private init; }
    public Guid ProductId { get; private set; }
    public string CombinationString { get; private set; } = "";
    public decimal Price { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }

    public List<GetProductImageDto>? Images { get; set; }
}
