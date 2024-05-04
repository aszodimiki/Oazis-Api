using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.ModelsBuilder;
using System;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Oazis.BLL.Services
{
    public class SiteService : ISiteService
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly IPublishedContentQuery _publishedContentQuery;

        public SiteService(IUmbracoContextAccessor umbracoContextAccessor, IPublishedValueFallback publishedValueFallback, IPublishedContentQuery publishedContentQuery)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _publishedValueFallback = publishedValueFallback;
            _publishedContentQuery = publishedContentQuery;
        }

        public async Task<IPublishedContent> GetRootSite()
        {
            try
            {
                var rootSite = _publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);

                return rootSite;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<IPublishedContent> GetRootFoodSiteByAlias(string modelTypeAlias)
        {
            try
            {
                var rootSite = _publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                var menuSite = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Menu.ModelTypeAlias);
                var foodsContent = menuSite.Children.FirstOrDefault(x => x.ContentType.Alias == modelTypeAlias);
                return foodsContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IPublishedContent> GetKitchen()
        {
            try
            {
                var rootSite = _publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Kitchen.ModelTypeAlias);

                return rootSite;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
