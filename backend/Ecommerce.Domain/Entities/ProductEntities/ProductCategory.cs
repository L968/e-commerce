namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductCategory : AuditableEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public List<Product>? Products { get; set; }
}