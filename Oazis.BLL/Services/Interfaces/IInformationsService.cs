using Oazis.Domain.Models;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IInformationsService
    {
        Task<InformationsDTO> GetAllInformation();
        Task<FooterTextsDTO> GetFooterTexts();
    }
}
