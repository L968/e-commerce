namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class ProductCombinationConfiguration : IEntityTypeConfiguration<ProductCombination>
{
    public void Configure(EntityTypeBuilder<ProductCombination> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Price)
            .HasPrecision(65, 2);

        builder
            .HasOne(productCombination => productCombination.Product)
            .WithMany(product => product.Combinations)
            .HasForeignKey(productCombination => productCombination.ProductId);
    }
}
