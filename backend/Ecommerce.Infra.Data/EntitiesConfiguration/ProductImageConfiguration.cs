namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder
            .HasOne(productImage => productImage.ProductCombination)
            .WithMany(productCombination => productCombination.Images)
            .HasForeignKey(productImage => productImage.ProductCombinationId);
    }
}
