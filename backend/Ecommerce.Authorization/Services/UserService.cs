using System.Web;
using System.Transactions;

namespace Authorization.Services
{
    public class UserService
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public UserService(UserManager<CustomIdentityUser> userManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CreateUser(CreateUserDto createUserDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var identityUser = new CustomIdentityUser()
            {
                UserName = createUserDto.Email!,
                Name = createUserDto.Name!,
                Email = createUserDto.Email!,
            };

            var identityResult = _userManager.CreateAsync(identityUser, createUserDto.Password).Result;

            if (!identityResult.Succeeded) return Result.Fail(identityResult.Errors.Where(error => !error.Description.Contains("Username")).FirstOrDefault()!.Description);

            _userManager.AddToRoleAsync(identityUser, "regular");

            string confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;
            string encodedCode = HttpUtility.UrlEncode(confirmationCode);
            _emailService.SendEmailConfirmationEmail(identityUser.Email, identityUser.Id, encodedCode);

            transaction.Complete();
            return Result.Ok();
        }

        public Result ActivateUser(ActivateUserRequest activateUserRequest)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == activateUserRequest.Id);

            if (identityUser == null) return Result.Fail("Error in activating user");

            var identityResult = _userManager.ConfirmEmailAsync(identityUser, activateUserRequest.ConfirmationCode).Result;

            if (!identityResult.Succeeded) return Result.Fail("Error in activating user");

            return Result.Ok();
        }
    }
}