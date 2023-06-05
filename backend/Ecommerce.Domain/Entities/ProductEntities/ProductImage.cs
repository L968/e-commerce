namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductImage
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public string ImagePath { get; private set; }

    public Product? Product { get; set; }

    public ProductImage(int productId, string imagePath)
    {
        ProductId = productId;
        ImagePath = imagePath;
    }

    public ProductImage(int id, int productId, string imagePath)
    {
        Id = id;
        ProductId = productId;
        ImagePath = imagePath;
    }
}