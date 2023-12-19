namespace Ecommerce.Domain.Repositories;

public interface IBaseRepository<T>
{
    T Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
