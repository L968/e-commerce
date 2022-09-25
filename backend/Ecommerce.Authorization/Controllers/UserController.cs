using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Authorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto createUserDto)
        {
            Result result = _userService.CreateUser(createUserDto);

            if (result.IsFailed) return BadRequest(result.Reasons[0]);

            return NoContent();
        }

        [Authorize(Roles = "regular")]
        [HttpPatch("phoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdatePhoneNumber(string phoneNumber)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            Result result = await _userService.UpdatePhoneNumber(userId, phoneNumber);

            if (result.IsFailed) return BadRequest(result.Reasons[0]);

            return NoContent();
        }

        [Authorize(Roles = "regular")]
        [HttpPost("phoneNumber/confirm")]
        public async Task<IActionResult> ConfirmPhoneNumber([Required] string phoneNumber, [Required] string confirmationToken)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            Result result = await _userService.ConfirmPhoneNumber(userId, phoneNumber, confirmationToken);

            if (result.IsFailed) return BadRequest(result.Reasons[0]);

            return NoContent();
        }

        [HttpGet("activate")]
        public IActionResult Activate([FromQuery] ActivateUserRequest activateUserRequest)
        {
            Result result = _userService.ActivateUser(activateUserRequest);

            if (result.IsFailed) return StatusCode(500);

            return NoContent();
        }
    }
}