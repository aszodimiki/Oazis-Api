using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Oazis.BLL.Services
{
    public class DetailService : IDetailService
    {
        private readonly ISiteService _siteService;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public DetailService(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task<List<LinksDTO>> GetLinksAsync()
        {
            var rootSite = await _siteService.GetRootSite();
            var result = await GetLinksDto(rootSite);

            return result;
        }

        async Task<List<LinksDTO>> GetLinksDto(IPublishedContent rootSite)
        {
            var result = new List<LinksDTO>();
            foreach (var item in rootSite.Children.Where(x => x.IsComposedOf(MainNavigation.ModelTypeAlias)))
            {
                var link = new LinksDTO
                {
                    Name = item.Name,
                    Url = item.GetProperty("navigationURL")?.Value<string>(_publishedValueFallback) ?? string.Empty
                };
                result.Add(link);

                if (item.Children.Any(x => x.IsComposedOf(MainNavigation.ModelTypeAlias)))
                {
                    link.NestedLinks = await GetLinksDto(item);

                    //foreach (var nestedItem in item.Children)
                    //{
                    //    result.Add(new LinksDTO{Name = nestedItem.Name, Url = nestedItem.UrlSegment});
                    //}
                }
            }
            return result;
        }
    }
}
