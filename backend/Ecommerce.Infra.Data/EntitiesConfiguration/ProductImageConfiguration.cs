namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder
            .HasOne(productImage => productImage.Product)
            .WithMany(product => product.Images)
            .HasForeignKey(productImage => productImage.ProductId);
    }
}