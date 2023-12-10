using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariation>
{
    public void Configure(EntityTypeBuilder<ProductVariation> builder)
    {
        builder
            .HasOne(pv => pv.Product)
            .WithMany(p => p.Variations)
            .HasForeignKey(pv => pv.ProductId);

        builder
            .HasOne(pv => pv.Variant)
            .WithMany(v => v.ProductVariations)
            .HasForeignKey(pv => pv.VariantId);
    }
}
