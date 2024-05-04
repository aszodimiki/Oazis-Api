using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services.Interfaces
{
    public interface ISiteService
    {
        Task<IPublishedContent> GetRootSite();
        Task<IPublishedContent> GetRootFoodSiteByAlias(string siteName);
        Task<IPublishedContent> GetKitchen();
    }
}
