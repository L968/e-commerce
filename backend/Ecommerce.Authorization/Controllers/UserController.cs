namespace Authorization.Controllers
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

        [HttpGet("/activate")]
        public IActionResult Activate([FromQuery] ActivateUserRequest activateUserRequest)
        {
            Result result = _userService.ActivateUser(activateUserRequest);

            if (result.IsFailed) return StatusCode(500);

            return NoContent();
        }
    }
}