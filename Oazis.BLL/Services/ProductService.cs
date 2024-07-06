using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.Models.Product;
using Oazis.Domain.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class ProductService(
        IPublishedContentQuery publishedContentQuery,
        IMapper mapper,
        IPublishedValueFallback publishedValueFallback)
        : IProductService
    {

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

        public async Task<List<DrinkTypeDto>> GetDrinkTypes()
        {
            var kitchen = publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Kitchen.ModelTypeAlias);
            var productTypesParent = kitchen?.Children.FirstOrDefault(x => x.ContentType.Alias == DrinkTypes.ModelTypeAlias);
            var result = productTypesParent?.Children.Where(x => x.ContentType.Alias == DrinkType.ModelTypeAlias)
                .Select(mapper.Map<DrinkTypeDto>).ToList();

            return result;
        }

        public async Task<List<DrinkDto>> GetDrinksByType(string type)
        {
            try
            {
                var rootSite = publishedContentQuery.ContentAtRoot().FirstOrDefault(x =>
                    x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                var drinkSites = rootSite?.Children.FirstOrDefault(x => x.ContentType.Alias == Drinks.ModelTypeAlias);
               var drinkGroups = drinkSites?.Children.Where(x => x.ContentType.Alias == DrinkGroup.ModelTypeAlias)
                    .Select(x => new DrinkGroup(x, publishedValueFallback))?.FirstOrDefault(x =>
                        new DrinkType(x.TypeOfDrinks, publishedValueFallback).TypeName == type);
                var drinks = drinkGroups?.Children.Select(x => new Drink(x, publishedValueFallback));

                var drinkDtos = drinks?.Select(mapper.Map<DrinkDto>).ToList() ?? [];
                return drinkDtos;
            }
            catch (Exception ex)
            {
                return [];
            }

        }
    }
}
