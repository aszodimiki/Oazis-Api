using Oazis.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IPizzaService
    {
        Task<List<PizzaDTO>> GetAllPizza();
    }
}
