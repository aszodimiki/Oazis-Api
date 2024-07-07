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
            if (kitchen == null) return [];

            var productTypesParent = kitchen.Children.FirstOrDefault(x => x.ContentType.Alias == ProductTypes.ModelTypeAlias);
            if (productTypesParent == null) return [];

            var result = productTypesParent.Children
                .Where(x => x.ContentType.Alias == ProductType.ModelTypeAlias)
                .Select(mapper.Map<ProductTypeDTO>)
                .ToList();

            return result;
        }

        public async Task<List<ProductDto>> GetProductsByType(string type)
        {
            try
            {
                var rootSite = publishedContentQuery.ContentAtRoot().FirstOrDefault(x =>
                    x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                if (rootSite == null) return [];

                var menuSite = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Menu.ModelTypeAlias);
                if (menuSite == null) return [];

                var productType = menuSite.Children
                    .Select(x => new Products(x, publishedValueFallback))
                    .FirstOrDefault(x => new ProductType(x, publishedValueFallback).TypeName == type);

                if (productType == null) return [];

                var productDtos = productType.Children
                    .Select(x => new Product(x, publishedValueFallback))
                    .Select(mapper.Map<ProductDto>)
                    .ToList();

                return productDtos;
            }
            catch (Exception ex)
            {
                return [];
            }

        }

        public async Task<List<DrinkTypeDto>> GetDrinkTypes()
        {
            try
            {
                var kitchen = publishedContentQuery.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == Kitchen.ModelTypeAlias);
                if (kitchen == null) return [];

                var productTypesParent = kitchen.Children.FirstOrDefault(x => x.ContentType.Alias == DrinkTypes.ModelTypeAlias);
                if (productTypesParent == null) return [];

                var result = productTypesParent.Children
                    .Where(x => x.ContentType.Alias == DrinkType.ModelTypeAlias)
                    .Select(mapper.Map<DrinkTypeDto>)
                    .ToList();

                return result;
            }
            catch (Exception e)
            {
                return [];
            }
            
        }

        public async Task<List<DrinkDto>> GetDrinksByType(string type)
        {
            try
            {
                var rootSite = publishedContentQuery.ContentAtRoot().FirstOrDefault(x =>
                    x.ContentType.Alias == Domain.ModelsBuilder.Oazis.ModelTypeAlias);
                if (rootSite == null) return [];

                var drinkSites = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Drinks.ModelTypeAlias);
                if (drinkSites == null) return [];

                var drinkGroup = drinkSites.Children
                    .Select(x => new DrinkGroup(x, publishedValueFallback))
                    .FirstOrDefault(x => new DrinkType(x.TypeOfDrinks, publishedValueFallback).TypeName == type);

                if (drinkGroup == null) return [];

                var drinkDtos = drinkGroup.Children
                    .Select(x => new Drink(x, publishedValueFallback))
                    .Select(mapper.Map<DrinkDto>)
                    .ToList();

                return drinkDtos;
            }
            catch (Exception ex)
            {
                return [];
            }

        }
    }
}
