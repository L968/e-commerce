namespace Ecommerce.Authorization.Errors;

public static class ServiceErrors
{
    public static class LoginService
    {
        public static readonly Error InvalidCredentials = new("Your login credentials don't match an account in our system");
        public static readonly Error EmailNotConfirmed = new("Email isn't confirmed");
        public static readonly Error UserLockedOut = new("User is currently locked out");
        public static readonly Error TwoFactorLoginRequiresPhoneNumber = new("Two factor login requires phone number");
        public static readonly Error UserRequiresTwoFactorAuthentication = new("User requires two factor authentication");
        public static readonly Error ErrorInTwoFactorLogin = new("Error in two factor login");
        public static readonly Error ErrorInPasswordReset = new("Error in password reset");
    }

    public static class UserService
    {
        public static readonly Error InvalidAddressIdValue = new("Invalid address id value");
        public static readonly Error PhoneNumberConfirmationRequiredForTwoFactorActivation = new("You must have a confirmed phone number in order to activate two factor authentication");
        public static readonly Error ErrorInUpdatingTwoFactorAuthentication = new("Error in updating two factor authentication");
        public static readonly Error PhoneNumberIsRequired = new("Phone number is required");
        public static readonly Error PhoneNumberIsAlreadyTaken = new("Phone number is already taken");
        public static readonly Error ErrorInUpdatingPhoneNumber = new("Error in updating phone number");
        public static readonly Error ErrorInConfirmingPhoneNumber = new("Error in confirming phone number");
        public static readonly Error ErrorInActivatingUser = new("Error in activating user");
    }

    public static Error NotFound(string entityName, int id)
    {
        return new Error($"{entityName} with ID {id} not found");
    }

    public static Error NotFound(string entityName, Guid guid)
    {
        return new Error($"{entityName} with Guid {guid} not found");
    }
}
