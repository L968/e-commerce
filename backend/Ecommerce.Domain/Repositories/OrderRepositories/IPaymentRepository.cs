namespace Ecommerce.Domain.Repositories.OrderRepositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);
    Task<Payment> CreateAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Payment payment);
}