using Oazis.Domain.Models;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IDetailService
    {
        Task<List<LinksDTO>> GetLinksAsync();
    }
}
