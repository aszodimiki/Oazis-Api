using Oazis.Domain.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductTypeDTO>> GetProductTypes();
        Task<List<ProductDto>> GetProductsByType(string type);
    }
}
