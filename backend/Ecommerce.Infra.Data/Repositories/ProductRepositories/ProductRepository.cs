﻿namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.Reviews)
            .Include(p => p.Discounts)
            .Include(p => p.Variations)
                .ThenInclude(pv => pv.Variant)
            .Include(p => p.Variations)
                .ThenInclude(pv => pv.VariationOptions)
                .ThenInclude(pvo => pvo.VariantOption)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.Discounts)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Inventory)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.Variations)
                .ThenInclude(pv => pv.Variant)
            .Include(p => p.Variations)
                .ThenInclude(pv => pv.VariationOptions)
                .ThenInclude(pvo => pvo.VariantOption)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Product Create(Product product)
    {
        _context.Products.Add(product);
        return product;
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}
