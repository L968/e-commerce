namespace Ecommerce.Infra.Data.Repositories.OrderRepositories;

public class PaymentRepository : IPaymentRepository
{
    public Task<Payment> CreateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Payment>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }
}