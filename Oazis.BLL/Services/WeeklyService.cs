using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class WeeklyService(IPublishedValueFallback publishedValueFallback, ISiteService siteService, IMapper mapper)
        : IWeeklyService
    {

        public async Task<WeeklyMenuDto> GetWeeklyMenu()
        {
            var rootSite = await siteService.GetRootSite();
            var weeklyRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == WeeklyMenu.ModelTypeAlias);
            var weeklyMenus = mapper.Map<WeeklyMenuDto>(weeklyRoot);

            var weeklyGroups = weeklyRoot?.Children.Where(x => x.ContentType.Alias == WeeklyGroup.ModelTypeAlias).ToList();

            foreach (var weeklyGroup in weeklyGroups ?? [])
            {
                var dailyMenus = weeklyGroup.Children.Select(mapper.Map<DailyMenuDto>).ToList();
                var weeklyGroupDto = new WeeklyGroupDto
                {
                    WeeklyTitle = weeklyGroup.Name,
                    DailyMenus = dailyMenus
                };
                weeklyMenus.WeeklyGroups.Add(weeklyGroupDto);
            }

            return weeklyMenus;

        }
    }
}
