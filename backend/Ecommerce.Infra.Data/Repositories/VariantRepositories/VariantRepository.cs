using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class VariantRepository : IVariantRepository
{
    private readonly AppDbContext _context;

    public VariantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Variant>> GetAllAsync()
    {
        return await _context.Variants
            .Include(v => v.Options)
            .ToListAsync();
    }
}
