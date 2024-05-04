using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class WeeklyService : IWeeklyService
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public WeeklyService(IPublishedValueFallback publishedValueFallback, ISiteService siteService, IMapper mapper)
        {
            _publishedValueFallback = publishedValueFallback;
            _siteService = siteService;
            _mapper = mapper;
        }

        public async Task<WeeklyMenuDto> GetWeeklyMenu()
        {
            var rootSite = await _siteService.GetRootSite();
            var weeklyRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == WeeklyMenu.ModelTypeAlias);
            var weeklyMenus = _mapper.Map<WeeklyMenuDto>(weeklyRoot);

            var weeklyGroups = weeklyRoot.Children.Where(x => x.ContentType.Alias == WeeklyGroup.ModelTypeAlias).ToList();

            foreach (var weeklyGroup in weeklyGroups)
            {
                var dailyMenus = weeklyGroup.Children.Select(x => _mapper.Map<DailyMenuDto>(x)).ToList();
                var weeklyGroupDto = new WeeklyGroupDto
                {
                    DailyMenus = dailyMenus
                };
                weeklyMenus.WeeklyGroups.Add(weeklyGroupDto);
            }

            return weeklyMenus;
            
        }
    }
}
