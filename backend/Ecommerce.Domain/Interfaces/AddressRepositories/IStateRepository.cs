namespace Ecommerce.Domain.Interfaces.AddressRepositories;

public interface IStateRepository
{
    Task<IEnumerable<State>> GetAllAsync();
    Task<State?> GetByIdAsync(int id);
    Task<State> CreateAsync(State state);
    Task UpdateAsync(State state);
    Task DeleteAsync(State state);
}