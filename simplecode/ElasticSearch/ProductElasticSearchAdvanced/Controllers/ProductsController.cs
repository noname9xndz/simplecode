using Microsoft.AspNetCore.Mvc;
using ProductElasticSearchAdvanced.Models;
using ProductElasticSearchAdvanced.Services;
using System.Threading.Tasks;

namespace ProductElasticSearchAdvanced.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var existing = await _productService.GetProductByIdCache(id);

            if (existing != null)
            {
                await _productService.SaveSingleAsync(product);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existing = await _productService.GetProductByIdCache(id);

            if (existing != null)
            {
                await _productService.DeleteAsync(existing);
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("fakeimport/{count}")]
        public async Task<ActionResult> Import(int count = 0)
        {
            var res = await _productService.GenerateProducts(count);
            if (res > 0)
                return Ok();
            return BadRequest();
        }
    }
}