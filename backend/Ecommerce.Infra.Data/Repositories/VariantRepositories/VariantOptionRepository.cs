using Ecommerce.Domain.Entities.VariantEntities;
using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class VariantOptionRepository : IVariantOptionRepository
{
    private readonly AppDbContext _context;

    public VariantOptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VariantOption>> GetAllAsync()
    {
        return await _context.VariantOptions
            .Include(v => v.Variant)
            .ToListAsync();
    }

    public async Task<VariantOption?> GetByIdAsync(int id)
    {
        return await _context.VariantOptions
            .Include(v => v.Variant)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public VariantOption Create(VariantOption variantOption)
    {
        _context.VariantOptions.Add(variantOption);
        return variantOption;
    }

    public void Update(VariantOption variantOption)
    {
        _context.VariantOptions.Update(variantOption);
    }

    public void Delete(VariantOption variantOption)
    {
        _context.VariantOptions.Remove(variantOption);
    }
}
