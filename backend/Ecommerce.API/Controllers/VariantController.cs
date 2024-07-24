using Ecommerce.Application.DTOs.Variants;
using Ecommerce.Application.Features.Variants.Commands.CreateVariant;
using Ecommerce.Application.Features.Variants.Commands.UpdateVariant;
using Ecommerce.Application.Features.Variants.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class VariantController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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
            GetVariantDto variant = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), null, variant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVariantCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
