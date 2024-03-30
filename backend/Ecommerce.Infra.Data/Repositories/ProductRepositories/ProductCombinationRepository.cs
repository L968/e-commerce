﻿namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCombinationRepository(AppDbContext context) : BaseRepository<ProductCombination>(context), IProductCombinationRepository
{
    public async override Task<ProductCombination?> GetByIdAsync(Guid id)
    {
        return await _context.ProductCombinations
            .Include(pc => pc.Product)
                .ThenInclude(p => p.Combinations)
            .Include(pc => pc.Images)
            .FirstOrDefaultAsync(pc => pc.Id == id);
    }
}
