using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByUserIdAsync(int userId);
    Task<Address?> GetByIdAndUserIdAsync(int id, int userId);
    Task<Address> CreateAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
}