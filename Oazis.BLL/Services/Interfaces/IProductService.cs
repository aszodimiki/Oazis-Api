using Oazis.Domain.Models;
using Oazis.Domain.Models.Product;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductTypeDTO>> GetProductTypes();
        Task<List<ProductDto>> GetProductsByType(string type);
        Task<List<DrinkTypeDto>> GetDrinkTypes();
        Task<List<DrinkDto>> GetDrinksByType(string type);
    }
}
