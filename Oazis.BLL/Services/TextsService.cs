using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Extensions;

namespace Oazis.BLL.Services
{
    public class TextsService : ITextsService
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        public TextsService(IMapper mapper, ISiteService siteService, IPublishedValueFallback publishedValueFallback, IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _mapper = mapper;
            _siteService = siteService;
            _publishedValueFallback = publishedValueFallback;
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
        }


        public async Task<List<CarouselItemDto>> GetCarousels()
        {
            var rootSite = await _siteService.GetRootSite();
            var carouselsRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Carousel.ModelTypeAlias);

            var carousels = carouselsRoot.Children.Where(x => x.ContentType.Alias == CarouselItem.ModelTypeAlias).ToList();

            var carouselsitems = carousels.Select(x => _mapper.Map<CarouselItemDto>(x)).ToList();

            return carouselsitems;

        }

        public async Task<List<GridBlockDto>> GetHomeTexts()
        {
            var results = new List<GridBlockDto>();
            var rootSite = await _siteService.GetRootSite();
            var homeRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Home.ModelTypeAlias);
            var home = new Home(homeRoot, _publishedValueFallback);
            var homeGrid = home.GridBlock;

            return homeGrid.Select(x => _mapper.Map<GridBlockDto>(x)).ToList();
        }

        public async Task<List<GridBlockDto>> GetWeeklyMenuTexts()
        {
            var results = new List<GridBlockDto>();
            var rootSite = await _siteService.GetRootSite();
            var weeklyRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == WeeklyMenu.ModelTypeAlias);
            var weekly = new WeeklyMenu(weeklyRoot, _publishedValueFallback);
            var weeklyGrid = weekly.GridBlock;

            return weeklyGrid.Select(x => _mapper.Map<GridBlockDto>(x)).ToList();
        }
    }
}
