using Oazis.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface ITextsService
    {
        Task<List<CarouselItemDto>> GetCarousels();
        Task<List<GridBlockDto>> GetHomeTexts();
        Task<List<GridBlockDto>> GetWeeklyMenuTexts();
    }
}
