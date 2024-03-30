using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Infra.Data.Repositories;

public class AddressRepository(AppDbContext context) : IAddressRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Address>> GetByUserIdAsync(int userId)
    {
        return await _context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address?> GetByIdAndUserIdAsync(int id, int userId)
    {
        return await _context.Addresses.FirstOrDefaultAsync(address => address.Id == id && address.UserId == userId);
    }

    public Address Create(Address address)
    {
        _context.Addresses.Add(address);
        return address;
    }

    public void Update(Address address)
    {
        _context.Addresses.Update(address);
    }

    public void Delete(Address address)
    {
        _context.Addresses.Remove(address);
    }
}
