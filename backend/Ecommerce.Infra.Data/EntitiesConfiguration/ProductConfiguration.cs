namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Price)
            .HasPrecision(65, 2);

        builder
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.ProductCategoryId);
    }
}