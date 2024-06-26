using Ecommerce.Application.DTOs.Products;
using Ecommerce.Domain.DTOs;

namespace Ecommerce.Order.API.Interfaces;

public interface IEcommerceService
{
    Task<CreateOrderAddressDto?> GetAddressByIdAsync(Guid id);
    Task<CreateOrderProductCombinationDto?> GetProductCombinationByIdAsync(Guid id);
    Task ClearCartAsync(IEnumerable<Guid> productCombinationIds);
    Task ReduceStockProductCombination(IEnumerable<ReduceStockRequest> requests);
}
