using Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Commands.UpdateAddress;
using Ecommerce.Application.Features.Addresses.Queries;
using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromQuery] GetAddressByUserIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetAddressDto? address = await _mediator.Send(new GetAddressByIdAndUserIdQuery(id));

            if (address is null) return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
        {
            Result<GetAddressDto> result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);
            var address = result.Value;

            return CreatedAtAction(nameof(GetById), new { address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressCommand command)
        {
            command.Id = id;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await _mediator.Send(new DeleteAddressCommand(id));

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
