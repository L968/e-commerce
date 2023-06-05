namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class Product : BaseEntity
{
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Sku { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
    public bool Visible { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public int ProductCategoryId { get; private set; }

    public ProductCategory? ProductCategory { get; set; }
    public ProductInventory? ProductInventory { get; set; }
    public List<ProductImage>? Images { get; set; }
}