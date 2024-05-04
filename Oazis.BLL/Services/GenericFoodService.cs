using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class GenericFoodService<T> : IGenericFoodService<T> where T : class
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public GenericFoodService(IPublishedValueFallback publishedValueFallback, ISiteService siteService, IMapper mapper)
        {
            _publishedValueFallback = publishedValueFallback;
            _siteService = siteService;
            _mapper = mapper;
        }

        public async Task<List<Tdto>> GetAllFoodByType<Tdto>(string modelTypeAlias)
        {
            // TODO CHECK
            var pizzasRootContent = await _siteService.GetRootFoodSiteByAlias(modelTypeAlias);
            var publishedPizzas = pizzasRootContent.Children.Select(x => (T)Activator.CreateInstance(typeof(T), new object[] { x, _publishedValueFallback })!);
            var result = publishedPizzas.Select(x => _mapper.Map<Tdto>(x));

            return result.ToList();
        }
    }
}
