using Ecommerce.Application.DTO.ProductDto;
using Ecommerce.Application.Interfaces.ProductServices;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            GetProductDto? product = await _productService.GetByGuidAsync(guid);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto productDto)
        {
            Result<GetProductDto> result = await _productService.CreateAsync(productDto);

            if (result.IsFailed) return BadRequest(result.Errors[0]);

            var product = result.Value;

            return CreatedAtAction(nameof(Get), new { product.Guid }, product);
        }

        [HttpPut("{guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdateProductDto productDto)
        {
            Result result = await _productService.UpdateAsync(guid, productDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            Result result = await _productService.DeleteAsync(guid);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}