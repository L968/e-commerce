namespace Ecommerce.Authorization.Interfaces;

public interface IUserService
{
    Task<CustomIdentityUser?> GetUserByIdAsync(int userId);
    Task<Guid?> GetDefaultAddressIdAsync(int userId);
    Task<Result> CreateUserAsync(CreateUserDto createUserDto);
    Task<Result> UpdateDefaultAddressAsync(Guid? addressId, int userId);
    Task<Result> UpdateTwoFactorAuthenticationAsync(int userId, bool twoFactorEnabled);
    Task<Result> UpdatePhoneNumberAsync(int userId, string phoneNumber);
    Task<Result> ConfirmPhoneNumberAsync(int userId, string phoneNumber, string confirmationToken);
    Task<Result> ActivateUserAsync(ActivateUserRequest activateUserRequest);
}
