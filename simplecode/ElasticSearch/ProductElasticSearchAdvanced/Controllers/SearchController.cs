using Microsoft.AspNetCore.Mvc;
using ProductElasticSearchAdvanced.Services;
using System.Threading.Tasks;

namespace ProductElasticSearchAdvanced.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductService _productService;

        public SearchController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await _productService.FindProducts(query, page, pageSize);

            return Ok(response);
        }

        [HttpGet("aggregations")]
        public async Task<IActionResult> Aggregations(string query)
        {
            var response = await _productService.GetAggregationsProduct(query);
            return Ok(response);
        }

        //Only for development purpose
        [HttpGet("reindex")]
        public async Task<IActionResult> ReIndex()
        {
            var response = await _productService.ReIndexProduct();

            return Ok($"{response} product(s) reindexed");
        }

        [HttpGet("productsetting")]
        public async Task<IActionResult> ProductSetting()
        {
             var settings = await _productService.GetProductSetting();
             return Ok(settings);

        }
    }
}