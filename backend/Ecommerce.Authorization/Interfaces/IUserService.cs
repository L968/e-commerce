namespace Ecommerce.Authorization.Interfaces;

public interface IUserService
{
    Result CreateUser(CreateUserDto createUserDto);
    Result UpdatePhoneNumber(int userId, string phoneNumber);
    Result UpdateTwoFactorAuthentication(int userId, bool twoFactorEnabled);
    Result ConfirmPhoneNumber(int userId, string phoneNumber, string confirmationToken);
    Result ActivateUser(ActivateUserRequest activateUserRequest);
    Task<Guid?> GetDefaultAddressId(int userId);
    Task<Result> UpdateDefaultAddress(Guid? addressId, int userId);
}
