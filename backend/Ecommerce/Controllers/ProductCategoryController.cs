namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productCategoryService.Get());
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            GetProductCategoryDto? productCategory = _productCategoryService.Get(guid);

            if (productCategory == null) return NotFound();

            return Ok(productCategory);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] CreateProductCategoryDto productCategoryDto)
        {
            var productCategory = _productCategoryService.Create(productCategoryDto);

            return CreatedAtAction(nameof(Get), new { productCategory.Guid }, productCategory);
        }

        [HttpPut("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(Guid guid, [FromBody] UpdateProductCategoryDto productCategoryDto)
        {
            Result result = _productCategoryService.Update(guid, productCategoryDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid guid)
        {
            Result result = _productCategoryService.Delete(guid);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}