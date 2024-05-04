using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Web.Common.Controllers;

namespace Oazis.Api.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController(IGenericFoodService<Pizza> genericPizzaService) : UmbracoApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await genericPizzaService.GetAllFoodByType<PizzaDTO>(Pizzas.ModelTypeAlias);
            return Ok(result);
        }
    }
}
