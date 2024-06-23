using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models.Product;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class ProductService(
        ISiteService siteService,
        IPublishedContentQuery publishedContentQuery,
        IMapper mapper,
        IPublishedValueFallback publishedValueFallback)
        : IProductService
    {
        private readonly ISiteService _siteService = siteService;

        public async Task<List<ProductTypeDTO>> GetProductTypes()
        {
            var kitchen = publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Kitchen.ModelTypeAlias);
            var productTypesParent = kitchen?.Children.FirstOrDefault(x => x.ContentType.Alias == ProductTypes.ModelTypeAlias);
            var result = productTypesParent?.Children.Where(x => x.ContentType.Alias == ProductType.ModelTypeAlias)
                .Select(mapper.Map<ProductTypeDTO>).ToList();

            return result;
        }

        public async Task<List<ProductDto>> GetProductsByType(string type)
        {
            try
            {
                var rootSite = publishedContentQuery.ContentAtRoot().FirstOrDefault(x =>
                    x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                var menuSite = rootSite?.Children.FirstOrDefault(x => x.ContentType.Alias == Menu.ModelTypeAlias);
                //var productTypes2 = menuSite?.Children.Where(x => x.ContentType.Alias == Products.ModelTypeAlias).Select(x => new Products(x, _publishedValueFallback))?.FirstOrDefault(x => x.TypeOfProduct != null && x.TypeOfProduct.Any(t => new ProductType(t, _publishedValueFallback).TypeName == type));
                var productTypes = menuSite?.Children.Where(x => x.ContentType.Alias == Products.ModelTypeAlias)
                    .Select(x => new Products(x, publishedValueFallback))?.FirstOrDefault(x =>
                        new ProductType(x, publishedValueFallback).TypeName == type);
                var products = productTypes?.Children.Select(x => new Product(x, publishedValueFallback));

                var productDtos = products?.Select(mapper.Map<ProductDto>).ToList() ?? [];
                return productDtos;
            }
            catch (Exception ex)
            {
                return [];
            }

        }
    }
}
