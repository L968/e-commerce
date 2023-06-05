namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .HasOne(address => address.City)
            .WithMany(city => city.Addresses)
            .HasForeignKey(address => address.CityId);
    }
}