using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Repositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByUserIdAsync(int userId);
    Task<Address?> GetByIdAndUserIdAsync(int id, int userId);
    Address Create(Address address);
    void Update(Address address);
    void Delete(Address address);
}
