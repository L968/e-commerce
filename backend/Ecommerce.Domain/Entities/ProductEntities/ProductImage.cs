namespace Ecommerce.Domain.Entities.ProductEntities;

public sealed class ProductImage
{
    public int Id { get; private set; }
    public Guid ProductCombinationId { get; private set; }
    public string ImagePath { get; private set; }

    public ProductCombination? ProductCombination { get; set; }

    public ProductImage(Guid productCombinationId, string imagePath)
    {
        ProductCombinationId = productCombinationId;
        ImagePath = imagePath;
    }
}
