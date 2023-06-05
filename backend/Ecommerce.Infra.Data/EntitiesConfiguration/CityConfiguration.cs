namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasOne(city => city.State)
            .WithMany(state => state.Cities)
            .HasForeignKey(city => city.StateId);
    }
}