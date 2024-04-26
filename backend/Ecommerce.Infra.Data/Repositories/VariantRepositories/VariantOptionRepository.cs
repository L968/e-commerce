namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class VariantOptionRepository(AppDbContext context) : BaseRepository<AppDbContext, VariantOption>(context), IVariantOptionRepository
{
    public override async Task<IEnumerable<VariantOption>> GetAllAsync()
    {
        return await _context.VariantOptions
            .Include(v => v.Variant)
            .ToListAsync();
    }

    public override async Task<VariantOption?> GetByIdAsync(int id)
    {
        return await _context.VariantOptions
            .Include(v => v.Variant)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}
