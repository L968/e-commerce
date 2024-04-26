namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class VariantRepository(AppDbContext context) : BaseRepository<AppDbContext, Variant>(context), IVariantRepository
{
    public override async Task<IEnumerable<Variant>> GetAllAsync()
    {
        return await _context.Variants
            .Include(v => v.Options)
            .ToListAsync();
    }

    public override async Task<Variant?> GetByIdAsync(Guid id)
    {
        return await _context.Variants
            .Include(v => v.Options)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}
