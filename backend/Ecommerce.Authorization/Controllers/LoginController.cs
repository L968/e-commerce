namespace Ecommerce.Authorization.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(LoginService loginService) : ControllerBase
{
    private readonly LoginService _loginService = loginService;

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        Result result = _loginService.Login(loginRequest);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return Ok(result.Successes[0]);
    }

    [HttpPost("twoFactorLogin")]
    public IActionResult TwoFactorLogin([FromBody] TwoFactorLoginRequest twoFactorLoginRequest)
    {
        Result result = _loginService.TwoFactorLogin(twoFactorLoginRequest);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return Ok(result.Successes[0]);
    }

    [HttpPost("/request-password-reset")]
    public IActionResult RequestPasswordReset([FromBody] RequestPasswordResetRequest request)
    {
        _loginService.RequestPasswordReset(request);
        return NoContent();
    }

    [HttpPost("/password-reset")]
    public IActionResult PasswordReset([FromBody] PasswordResetRequest request)
    {
        Result result = _loginService.PasswordReset(request);

        if (result.IsFailed) return Unauthorized(result.Errors[0]);

        return NoContent();
    }
}
