namespace Ecommerce.Application.Interfaces.AddressService;

public interface IAddressService
{
    public Task<IEnumerable<GetAddressDto>> GetByUserIdAsync(int userId);
    public Task<GetAddressDto?> GetByIdAndUserIdAsync(int id, int userId);
    public Task<GetAddressDto> CreateAsync(CreateAddressDto addressDto);
    public Task<Result> UpdateAsync(int id, int userId, UpdateAddressDto addressDto);
    public Task<Result> DeleteAsync(int id, int userId);
}