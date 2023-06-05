namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(new Country(id: 1, code: "BR", name: "Brazil"));
    }
}