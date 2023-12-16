using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductVariantOptionConfiguration : IEntityTypeConfiguration<ProductVariantOption>
{
    public void Configure(EntityTypeBuilder<ProductVariantOption> builder)
    {
        builder
            .HasOne(pvo => pvo.Product)
            .WithMany(p => p.VariantOptions)
            .HasForeignKey(pvo => pvo.ProductId);

        builder
            .HasOne(pvo => pvo.VariantOption)
            .WithMany(vo => vo.ProductVariantOptions)
            .HasForeignKey(pvo => pvo.VariantOptionId);
    }
}
