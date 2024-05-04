using System.Threading.Tasks;
using Oazis.Domain.Models;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IWeeklyService
    {
        Task<WeeklyMenuDto> GetWeeklyMenu();
    }
}
