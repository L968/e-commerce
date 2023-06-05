namespace Ecommerce.Infra.Data.Repositories.AddressRepositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>> GetByUserIdAsync(int userId)
    {
        return await _context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address?> GetByIdAndUserIdAsync(int id, int userId)
    {
        return await _context.Addresses.FindAsync(id, userId);
    }

    public async Task<Address> CreateAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Address address)
    {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }
}