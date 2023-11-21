namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        //builder
        //    .HasMany(cart => cart.CartItems)
        //    .WithOne(cartItems => cartItems.Cart)
        //    .HasForeignKey(cartItems => cartItems.CartId);
    }
}
