namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class VariantRepository : BaseRepository<Variant>, IVariantRepository
{
    public VariantRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Variant>> GetAllAsync()
    {
        return await _context.Variants
            .Include(v => v.Options)
            .ToListAsync();
    }

    public override async Task<Variant?> GetByIdAsync(int id)
    {
        return await _context.Variants
            .Include(v => v.Options)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}
