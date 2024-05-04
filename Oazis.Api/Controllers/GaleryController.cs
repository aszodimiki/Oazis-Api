using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;

namespace Oazis.Api.Controllers
{
    [Route("api/galery")]
    public class GaleryController(IImageService imageService) : ControllerBase
    {
        [HttpGet("images")]
        public async Task<IActionResult> GetAll()
        {
            var results = await imageService.GetPictures();
            return Ok(results);
        }
    }
}
