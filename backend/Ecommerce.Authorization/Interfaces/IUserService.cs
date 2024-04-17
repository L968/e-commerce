namespace Ecommerce.Authorization.Interfaces;

public interface IUserService
{
    Task<Result> CreateUserAsync(CreateUserDto createUserDto);
    Task<Result> UpdatePhoneNumberAsync(int userId, string phoneNumber);
    Task<Result> UpdateTwoFactorAuthenticationAsync(int userId, bool twoFactorEnabled);
    Task<Result> ConfirmPhoneNumberAsync(int userId, string phoneNumber, string confirmationToken);
    Task<Result> ActivateUserAsync(ActivateUserRequest activateUserRequest);
    Task<Guid?> GetDefaultAddressIdAsync(int userId);
    Task<Result> UpdateDefaultAddressAsync(Guid? addressId, int userId);
}
