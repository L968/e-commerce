namespace Ecommerce.Application.Common.Interfaces;

public interface IAuthorizationService
{
    Task<Guid?> GetDefaultAddressIdAsync();
    Task UpdateDefaultAddressIdAsync(Guid? addressId);
}
