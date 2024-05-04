using Oazis.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oazis.BLL.Services.Interfaces
{
    public interface IGenericFoodService<T> where T : class
    {
        Task<List<UDTO>> GetAllFoodByType<UDTO>(string modelTypeAlias);
    }
}
