namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.Get());
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            GetProductDto? product = _productService.Get(guid);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromForm] CreateProductDto productDto)
        {
            Result<GetProductDto> result = _productService.Create(productDto);

            if (result.IsFailed) return BadRequest(result.Errors[0]);

            var product = result.Value;

            return CreatedAtAction(nameof(Get), new { product.Guid }, product);
        }

        [HttpPut("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(Guid guid, [FromBody] UpdateProductDto productDto)
        {
            Result result = _productService.Update(guid, productDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid guid)
        {
            Result result = _productService.Delete(guid);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}