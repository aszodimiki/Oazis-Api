using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace Oazis.Api.Controllers
{
    [Route("api/information")]
    [ApiController]
    public class InformationController(IInformationsService informationService) : UmbracoApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await informationService.GetAllInformation();
            return Ok(result);
        }

        [HttpGet("footer")]
        public async Task<IActionResult> GetFooterTexts()
        {
            var result = await informationService.GetFooterTexts();
            return Ok(result);
        }
    }
}
