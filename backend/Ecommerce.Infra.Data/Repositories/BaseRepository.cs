using Ecommerce.Domain.Repositories;

namespace Ecommerce.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);

    }
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public virtual T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
