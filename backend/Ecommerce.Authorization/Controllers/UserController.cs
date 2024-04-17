using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Authorization.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [Authorize(Roles = "regular")]
    [HttpGet("defaultAddressId")]
    public async Task<IActionResult> GetDefaultAddress()
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Guid? addressId = await _userService.GetDefaultAddressId(userId);

        if (addressId is null) return NotFound();

        return Ok(addressId);
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
    [Route("defaultAddressId/")]
    [Route("defaultAddressId/{addressId}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateDefaultAddress(Guid? addressId)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = await _userService.UpdateDefaultAddress(addressId, userId);

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
