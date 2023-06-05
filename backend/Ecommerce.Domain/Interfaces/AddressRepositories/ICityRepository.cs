namespace Ecommerce.Domain.Interfaces.AddressRepositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City?> GetByIdAsync(int id);
    Task<City> CreateAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(City city);
}