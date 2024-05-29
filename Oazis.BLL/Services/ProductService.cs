using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models.Product;
using Oazis.Domain.ModelsBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly IPublishedContentQuery _publishedContentQuery;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public ProductService(ISiteService siteService, IPublishedContentQuery publishedContentQuery, IMapper mapper)
        {
            _siteService = siteService;
            _publishedContentQuery = publishedContentQuery;
            _mapper = mapper;
        }
        public async Task<List<ProductTypeDTO>> GetProductTypes()
        {
            var kitchen = _publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Kitchen.ModelTypeAlias);
            var productTypesParent = kitchen.Children.FirstOrDefault(x => x.ContentType.Alias == ProductTypes.ModelTypeAlias);
            var result = productTypesParent.Children.Where(x => x.ContentType.Alias == ProductType.ModelTypeAlias)
                .Select(x => _mapper.Map<ProductTypeDTO>(x)).ToList();

            return result;
        }

        public async Task<List<ProductDto>> GetProductsByType(string type)
        {
            try
            {
                var rootSite = _publishedContentQuery.ContentAtRoot().FirstOrDefault(x =>
                    x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                var menuSite = rootSite?.Children.FirstOrDefault(x => x.ContentType.Alias == Menu.ModelTypeAlias);
                //var productTypes2 = menuSite?.Children.Where(x => x.ContentType.Alias == Products.ModelTypeAlias).Select(x => new Products(x, _publishedValueFallback))?.FirstOrDefault(x => x.TypeOfProduct != null && x.TypeOfProduct.Any(t => new ProductType(t, _publishedValueFallback).TypeName == type));
                var productTypes = menuSite?.Children.Where(x => x.ContentType.Alias == Products.ModelTypeAlias)
                    .Select(x => new Products(x, _publishedValueFallback))?.FirstOrDefault(x =>
                        new ProductType(x, _publishedValueFallback).TypeName == type);
                var products = productTypes?.Children.Select(x => new Product(x, _publishedValueFallback));

                var productDtos = products?.Select(x => _mapper.Map<ProductDto>(x)).ToList() ?? [];
                return productDtos;
            }
            catch (Exception ex)
            {
                return [];
            }

        }
    }
}
