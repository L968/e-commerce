namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        builder
            .HasOne(productDiscount => productDiscount.Product)
            .WithMany(product => product.Discounts)
            .HasForeignKey(productDiscount => productDiscount.ProductId);

        builder.Property(pd => pd.DiscountValue).HasPrecision(65, 2);
        builder.Property(pd => pd.MaximumDiscountAmount).HasPrecision(65, 2);
    }
}
