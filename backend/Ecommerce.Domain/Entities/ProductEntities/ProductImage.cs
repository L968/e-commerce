namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductImage
{
    public int Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ImagePath { get; private set; }

    public Product? Product { get; set; }

    public ProductImage(Guid productId, string imagePath)
    {
        ProductId = productId;
        ImagePath = imagePath;
    }
}