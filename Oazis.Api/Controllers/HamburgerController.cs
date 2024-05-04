using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Web.Common.Controllers;

namespace Oazis.Api.Controllers
{
    [Route("api/hamburgers")]
    [ApiController]
    public class HamburgerController(IGenericFoodService<Hamburger> genericPizzaService) : UmbracoApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await genericPizzaService.GetAllFoodByType<HamburgerDTO>(Hamburgers.ModelTypeAlias);
            return Ok(result);
        }
    }
}
