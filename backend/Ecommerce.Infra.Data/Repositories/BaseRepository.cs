namespace Ecommerce.Infra.Data.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
