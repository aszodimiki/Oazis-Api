using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;

namespace Oazis.Api.Controllers
{
    [Route("api/texts")]
    public class TextsController(ITextsService textsService) : ControllerBase
    {
        [HttpGet("carousels")]
        public async Task<IActionResult> GetCarousels()
        {
            var result = await textsService.GetCarousels();
            return Ok(result);
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHome()
        {
            var result = await textsService.GetHomeTexts();
            return Ok(result);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeekly()
        {
            var result = await textsService.GetWeeklyMenuTexts();
            return Ok(result);
        }
    }
}
