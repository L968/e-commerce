namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class VariantOptionConfiguration : IEntityTypeConfiguration<VariantOption>
{
    public void Configure(EntityTypeBuilder<VariantOption> builder)
    {
        builder
            .HasOne(vo => vo.Variant)
            .WithMany(v => v.Options)
            .HasForeignKey(vo => vo.VariantId);
    }
}
