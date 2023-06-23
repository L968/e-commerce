namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductDiscountConfiguration : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        builder
            .HasOne(productDiscount => productDiscount.Product)
            .WithMany(product => product.Discounts)
            .HasForeignKey(productDiscount => productDiscount.ProductId);
    }
}