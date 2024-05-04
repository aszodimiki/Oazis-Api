using Microsoft.AspNetCore.Mvc;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Threading.Tasks;

namespace Oazis.Api.Controllers
{
    [Route("api/fried")]
    [ApiController]
    public class FriedController(IGenericFoodService<Fried> genericPizzaService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await genericPizzaService.GetAllFoodByType<FriedDTO>(Fried.ModelTypeAlias);
            return Ok(result);
        }
    }
}
