namespace Ecommerce.Domain.Repositories;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(Guid id);
    T Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
