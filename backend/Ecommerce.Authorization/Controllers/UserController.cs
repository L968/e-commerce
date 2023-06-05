using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Authorization.Controllers;

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
    public IActionResult UpdatePhoneNumber(string phoneNumber)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = _userService.UpdatePhoneNumber(userId, phoneNumber);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [Authorize(Roles = "regular")]
    [HttpPost("phoneNumber/confirm")]
    public IActionResult ConfirmPhoneNumber([Required] string phoneNumber, [Required] string confirmationToken)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = _userService.ConfirmPhoneNumber(userId, phoneNumber, confirmationToken);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [Authorize(Roles = "regular")]
    [HttpPatch("twoFactorEnabled/{twoFactorEnabled}")]
    public IActionResult UpdateTwoFactorAuthentication(bool twoFactorEnabled)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = _userService.UpdateTwoFactorAuthentication(userId, twoFactorEnabled);

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