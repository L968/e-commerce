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

    public async Task<Variant?> GetByIdAsync(int id)
    {
        return await _context.Variants
            .Include(v => v.Options)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Variant Create(Variant variant)
    {
        _context.Variants.Add(variant);
        return variant;
    }

    public void Update(Variant variant)
    {
        _context.Variants.Update(variant);
    }

    public void Delete(Variant variant)
    {
        _context.Variants.Remove(variant);
    }
}
