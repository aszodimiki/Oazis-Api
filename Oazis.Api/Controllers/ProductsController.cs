using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;

namespace Oazis.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService, IWeeklyService weeklyService)
        : Controller
    {
        [HttpGet("product-types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var result = await productService.GetProductTypes();

            return Ok(result);
        }

        [HttpGet("product-types/{type}")]
        public async Task<IActionResult> GetProductByType(string type)
        {
            var result = await productService.GetProductsByType(type);

            return Ok(result);
        }

        [HttpGet("drink-types")]
        public async Task<IActionResult> GetDrinkTypes()
        {
            var result = await productService.GetDrinkTypes();

            return Ok(result);
        }

        [HttpGet("drink-types/{type}")]
        public async Task<IActionResult> GetDrinksByType(string type)
        {
            var result = await productService.GetDrinksByType(type);

            return Ok(result);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyMenu()
        {
            var result = await weeklyService.GetWeeklyMenu();

            return Ok(result);
        }

    }
}
