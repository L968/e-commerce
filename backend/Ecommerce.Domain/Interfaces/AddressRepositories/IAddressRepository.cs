namespace Ecommerce.Domain.Interfaces.AddressRepositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByUserIdAsync(int userId);
    Task<Address?> GetByIdAndUserIdAsync(int id, int userId);
    Task<Address> CreateAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
}