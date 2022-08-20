namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class ProductInventoryController : ControllerBase
    {
        private readonly ProductInventoryService _productInventoryService;

        public ProductInventoryController(ProductInventoryService productInventoryService)
        {
            _productInventoryService = productInventoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productInventoryService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            GetProductInventoryDto? productInventory = _productInventoryService.Get(id);

            if (productInventory == null) return NotFound();

            return Ok(productInventory);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductInventoryDto productInventoryDto)
        {
            GetProductInventoryDto productInventory = _productInventoryService.Create(productInventoryDto);

            return CreatedAtAction(nameof(Get), new { productInventory.Id }, productInventory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] UpdateProductInventoryDto productInventoryDto)
        {
            Result result = _productInventoryService.Update(id, productInventoryDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Result result = _productInventoryService.Delete(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}