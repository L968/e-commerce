namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.ProductCategoryId);
    }
}
