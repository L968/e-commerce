using Ecommerce.Application.DTOs.Variants;
using Ecommerce.Application.Features.Variants.Commands.CreateVariant;
using Ecommerce.Application.Features.Variants.Commands.UpdateVariant;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetVariantDto? variant = await _mediator.Send(new GetVariantByIdQuery(id));

            if (variant is null) return NotFound();

            return Ok(variant);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateVariantCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Reasons);

            var variant = result.Value;
            return CreatedAtAction(nameof(Get), null, variant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVariantCommand command)
        {
            command.Id = id;
            Result result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                if (result.Reasons[0].Message.Contains("not found"))
                {
                    return NotFound();
                }

                return BadRequest(result.Reasons);
            }

            return NoContent();
        }
    }
}
