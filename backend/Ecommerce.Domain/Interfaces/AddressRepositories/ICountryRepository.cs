namespace Ecommerce.Domain.Interfaces.AddressRepositories;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<Country> CreateAsync(Country country);
    Task UpdateAsync(Country country);
    Task DeleteAsync(Country country);
}