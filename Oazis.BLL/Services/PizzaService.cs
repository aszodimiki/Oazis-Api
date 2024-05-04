using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPublishedContentQuery _publishedContentQuery;
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public PizzaService(IPublishedContentQuery publishedContentQuery, ISiteService siteService, IMapper mapper, IPublishedValueFallback publishedValueFallback)
        {
            _siteService = siteService;
            _mapper = mapper;
            _publishedValueFallback = publishedValueFallback;
        }

        public async Task<List<PizzaDTO>> GetAllPizza()
        {
            var pizzasRootContent = await _siteService.GetRootFoodSiteByAlias(Pizzas.ModelTypeAlias);
            var publishedPizzas = pizzasRootContent.Children.Select(x => new Pizza(x, _publishedValueFallback));
            var result = publishedPizzas.Select(x => _mapper.Map<PizzaDTO>(x));

            return result.ToList();
        }
    }
}
