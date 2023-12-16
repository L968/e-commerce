namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductCategoryVariantConfiguration : IEntityTypeConfiguration<ProductCategoryVariant>
{
    public void Configure(EntityTypeBuilder<ProductCategoryVariant> builder)
    {
        builder
            .HasOne(pcv => pcv.ProductCategory)
            .WithMany(p => p.Variants)
            .HasForeignKey(pcv => pcv.ProductCategoryId);

        builder
            .HasOne(pv => pv.Variant)
            .WithMany(v => v.ProductCategoryVariants)
            .HasForeignKey(pv => pv.VariantId);
    }
}
