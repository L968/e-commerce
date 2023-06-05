namespace Ecommerce.Infra.Data.Repositories.AddressRepositories;

public class CountryRepository : ICountryRepository
{
    public Task<Country> CreateAsync(Country country)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Country country)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Country>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Country?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Country country)
    {
        throw new NotImplementedException();
    }
}