﻿namespace Ecommerce.Application.Interfaces;

public interface IAuthorizationService
{
    Task<Guid?> GetDefaultAddressIdAsync();
    Task UpdateDefaultAddressIdAsync(Guid? addressId);
}
