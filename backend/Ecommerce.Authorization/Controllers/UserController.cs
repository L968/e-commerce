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
        Guid? addressId = await _userService.GetDefaultAddressIdAsync(userId);

        if (addressId is null) return NotFound();

        return Ok(addressId);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        Result result = await _userService.CreateUserAsync(createUserDto);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [Authorize(Roles = "regular")]
    [HttpPatch("phoneNumber/{phoneNumber}")]
    public async Task<IActionResult> UpdatePhoneNumber(string phoneNumber)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = await _userService.UpdatePhoneNumberAsync(userId, phoneNumber);

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
        Result result = await _userService.UpdateDefaultAddressAsync(addressId, userId);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [Authorize(Roles = "regular")]
    [HttpPost("phoneNumber/confirm")]
    public async Task<IActionResult> ConfirmPhoneNumber([Required] string phoneNumber, [Required] string confirmationToken)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = await _userService.ConfirmPhoneNumberAsync(userId, phoneNumber, confirmationToken);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [Authorize(Roles = "regular")]
    [HttpPatch("twoFactorEnabled/{twoFactorEnabled}")]
    public async Task<IActionResult> UpdateTwoFactorAuthentication(bool twoFactorEnabled)
    {
        int userId = int.Parse(User.FindFirst("id")!.Value);
        Result result = await _userService.UpdateTwoFactorAuthenticationAsync(userId, twoFactorEnabled);

        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [HttpGet("activate")]
    public async Task<IActionResult> Activate([FromQuery] ActivateUserRequest activateUserRequest)
    {
        Result result = await _userService.ActivateUserAsync(activateUserRequest);

        if (result.IsFailed) return StatusCode(500);

        return NoContent();
    }
}
