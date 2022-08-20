using Ecommerce.Data.DTO.Address;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            return Ok(_addressService.Get(userId));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            GetAddressDto? Address = _addressService.Get(id, userId);

            if (Address == null) return NotFound();

            return Ok(Address);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAddressDto addressDto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            addressDto.UserId = userId;
            var address = _addressService.Create(addressDto);

            return CreatedAtAction(nameof(Get), new { address.Id }, address);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAddressDto addressDto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            Result result = _addressService.Update(id, userId, addressDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            Result result = _addressService.Delete(id, userId);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}