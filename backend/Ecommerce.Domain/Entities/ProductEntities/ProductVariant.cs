namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductVariation
{
    public int Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int VariantId { get; private set; }
}
