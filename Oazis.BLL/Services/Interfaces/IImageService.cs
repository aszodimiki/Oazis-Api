using Oazis.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IImageService
    {
        Task<List<PictureDto>> GetPictures();
    }
}
