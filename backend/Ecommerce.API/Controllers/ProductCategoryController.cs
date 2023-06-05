using Ecommerce.Application.DTO.ProductDto;
using Ecommerce.Application.Interfaces.ProductServices;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] CreateProductCategoryDto productCategoryDto)
        {
            return Ok();
            //var productCategory = _productCategoryService.Create(productCategoryDto);

            //return CreatedAtAction(nameof(Get), new { productCategory.Guid }, productCategory);
        }

        [HttpPut("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(Guid guid, [FromBody] UpdateProductCategoryDto productCategoryDto)
        {
            return Ok();
            //Result result = _productCategoryService.Update(guid, productCategoryDto);

            //if (result.IsFailed) return NotFound();

            //return NoContent();
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid guid)
        {
            return Ok();
            //Result result = _productCategoryService.Delete(guid);

            //if (result.IsFailed) return NotFound();

            //return NoContent();
        }
    }
}