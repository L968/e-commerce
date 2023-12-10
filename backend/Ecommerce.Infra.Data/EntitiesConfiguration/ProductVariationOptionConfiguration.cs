using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductVariationOptionConfiguration : IEntityTypeConfiguration<ProductVariationOption>
{
    public void Configure(EntityTypeBuilder<ProductVariationOption> builder)
    {
        builder
            .HasOne(pvo => pvo.ProductVariation)
            .WithMany(pv => pv.VariationOptions)
            .HasForeignKey(pvo => pvo.ProductVariationId);

        builder
            .HasOne(pvo => pvo.VariantOption)
            .WithMany(vo => vo.ProductVariationOptions)
            .HasForeignKey(pvo => pvo.VariantOptionId);
    }
}
