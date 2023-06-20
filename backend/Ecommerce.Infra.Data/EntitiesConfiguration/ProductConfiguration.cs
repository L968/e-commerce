namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasOne(product => product.ProductInventory)
            .WithOne(productInventory => productInventory.Product)
            .HasForeignKey<ProductInventory>(productInventory => productInventory.ProductId);

        //builder
        //    .HasOne(product => product.ProductCategory)
        //    .WithMany(productCategory => productCategory.Products)
        //    .HasForeignKey(product => product.ProductCategoryId);
    }
}