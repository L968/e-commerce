using Ecommerce.Domain.DTOs;

namespace Ecommerce.Order.API.Services;

public interface IEcommerceService
{
    Task<CreateOrderAddressDto?> GetAddressByIdAsync(Guid id);
    Task<CreateOrderProductCombinationDto?> GetProductCombinationByIdAsync(Guid id);
    Task ClearCartAsync(IEnumerable<Guid> productCombinationIds);
}
