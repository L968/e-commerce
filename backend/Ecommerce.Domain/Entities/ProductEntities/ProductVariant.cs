namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductVariant
{
    public int Id { get; private set; }
    public string Type { get; private set; }
    public string Value { get; private set; }
    public int? ParentProductId { get; private set; }
    public int ProductId { get; private set; }

    public Product? Product { get; set; }
}