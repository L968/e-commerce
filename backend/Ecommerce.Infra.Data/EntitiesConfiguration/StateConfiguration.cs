namespace Ecommerce.Infra.Data.EntitiesConfiguration;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder
            .HasOne(state => state.Country)
            .WithMany(country => country.States)
            .HasForeignKey(state => state.CountryId);

        var brazilStates = new List<State>()
        {
            new State(id: 1,  code: "AC", name: "Acre", countryId: 1),
            new State(id: 2,  code: "AL", name: "Alagoas", countryId: 1),
            new State(id: 3,  code: "AP", name: "Amapá", countryId: 1),
            new State(id: 4,  code: "AM", name: "Amazonas", countryId: 1),
            new State(id: 5,  code: "BA", name: "Bahia", countryId: 1),
            new State(id: 6,  code: "CE", name: "Ceará", countryId: 1),
            new State(id: 7,  code: "ES", name: "Espírito Santo", countryId: 1),
            new State(id: 8,  code: "GO", name: "Goiás", countryId: 1),
            new State(id: 9,  code: "MA", name: "Maranhão", countryId: 1),
            new State(id: 10, code: "MT", name: "Mato Grosso", countryId: 1),
            new State(id: 11, code: "MS", name: "Mato Grosso do Sul ", countryId: 1),
            new State(id: 12, code: "MG", name: "Minas Gerais", countryId: 1),
            new State(id: 13, code: "PA", name: "Pará", countryId: 1),
            new State(id: 14, code: "PB", name: "Paraíba", countryId: 1),
            new State(id: 15, code: "PR", name: "Paraná", countryId: 1),
            new State(id: 16, code: "PE", name: "Pernambuco", countryId: 1),
            new State(id: 17, code: "PI", name: "Piauí", countryId: 1),
            new State(id: 18, code: "RJ", name: "Rio de Janeiro", countryId: 1),
            new State(id: 19, code: "RN", name: "Rio Grande do Norte", countryId: 1),
            new State(id: 20, code: "RS", name: "Rio Grande do Sul ", countryId: 1),
            new State(id: 21, code: "RO", name: "Rondônia", countryId: 1),
            new State(id: 22, code: "RR", name: "Roraima", countryId: 1),
            new State(id: 23, code: "SC", name: "Santa Catarina ", countryId: 1),
            new State(id: 24, code: "SP", name: "São Paulo", countryId: 1),
            new State(id: 25, code: "SE", name: "Sergipe", countryId: 1),
            new State(id: 26, code: "TO", name: "Tocantins", countryId: 1),
            new State(id: 27, code: "DF", name: "Distrito Federal ", countryId: 1)
        };

        builder.HasData(brazilStates);
    }
}
