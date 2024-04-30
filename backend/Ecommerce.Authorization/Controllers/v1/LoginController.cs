using Asp.Versioning;

namespace Ecommerce.Authorization.Controllers.v1;

[ApiController]
[ApiVersion(1)]
[Route("v{v:apiVersion}/[controller]")]
public class LoginController(ILoginService loginService) : ControllerBase
{
    private readonly ILoginService _loginService = loginService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        Result result = await _loginService.Login(loginRequest);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return Ok(result.Successes[0]);
    }

    [HttpPost("twoFactorLogin")]
    public async Task<IActionResult> TwoFactorLogin([FromBody] TwoFactorLoginRequest twoFactorLoginRequest)
    {
        Result result = await _loginService.TwoFactorLogin(twoFactorLoginRequest);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return Ok(result.Successes[0]);
    }

    [HttpPost("/v{v:apiVersion}/request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetRequest request)
    {
        await _loginService.RequestPasswordReset(request);
        return NoContent();
    }

    [HttpPost("/v{v:apiVersion}/password-reset")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetRequest request)
    {
        Result result = await _loginService.PasswordReset(request);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return NoContent();
    }
}
