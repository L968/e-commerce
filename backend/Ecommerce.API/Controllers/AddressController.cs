using Ecommerce.Application.DTO.AddressDto;
using Ecommerce.Application.Interfaces.AddressService;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            return Ok(await _addressService.GetByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            GetAddressDto? address = await _addressService.GetByIdAndUserIdAsync(id, userId);

            if (address == null) return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressDto addressDto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            addressDto.UserId = userId;
            GetAddressDto address = await _addressService.CreateAsync(addressDto);

            return CreatedAtAction(nameof(GetById), new { address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto addressDto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            Result result = await _addressService.UpdateAsync(id, userId, addressDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            Result result = await _addressService.DeleteAsync(id, userId);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}