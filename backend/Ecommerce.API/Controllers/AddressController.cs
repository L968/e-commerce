﻿using Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Commands.UpdateAddress;
using Ecommerce.Application.Features.Addresses.Queries;
using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class AddressController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromQuery] GetAddressByUserIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetAddressDto? address = await _mediator.Send(new GetAddressByIdAndUserIdQuery(id));

            if (address is null) return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
        {
            GetAddressDto address = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAddressCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteAddressCommand(id));
            return NoContent();
        }
    }
}
