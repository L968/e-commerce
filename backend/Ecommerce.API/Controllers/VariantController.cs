using Ecommerce.Application.Features.Variants.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class VariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetVariantQuery()));
        }
    }
}
