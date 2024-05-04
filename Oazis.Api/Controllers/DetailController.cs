using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;

namespace Oazis.Api.Controllers
{
    [Route("api/detail")]
    [ApiController]
    public class DetailController(IDetailService detailService) : ControllerBase
    {
        [HttpGet("home-page")]
        public async Task<IActionResult> GetHomePage()
        {
            return Ok();
        }

        [HttpGet("links")]
        public async Task<IActionResult> GetLinks()
        {
            var result = await detailService.GetLinksAsync();

            return Ok(result);
        }
    }
}
