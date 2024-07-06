using AutoMapper;
using Oazis.Domain.Models.Product;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Globalization;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Oazis.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientDTO>();

            CreateMap<Product, ProductDto>()
                .ForMember(q => q.Ingredients, y => y.MapFrom(t => t.Ingredients != null && t.Ingredients.Any() ? t.Ingredients.Select(x => x.Name) : Enumerable.Empty<string>()))
                .ForMember(q => q.SerialNumber, y => y.MapFrom(t => t.SerialNumber != null ? t.SerialNumber : 0))
                .ForMember(q => q.SecondPrice, y => y.MapFrom(t => t.SecondPrice != null ? t.SecondPrice : 0));

            CreateMap<ProductType, ProductTypeDTO>()
                .ForMember(q => q.NameOfProduct, y => y.MapFrom(t => t.Name));

            CreateMap<Drink, DrinkDto>();

            CreateMap<DrinkType, DrinkTypeDto>()
                .ForMember(q => q.NameOfProduct, y => y.MapFrom(t => t.Name));

            CreateMap<Information, InformationsDTO>();

            CreateMap<FooterTexts, FooterTextsDTO>();

            CreateMap<WeeklyMenu, WeeklyMenuDto>();

            CreateMap<DailyMenu, DailyMenuDto>()
                .ForMember(q => q.Day, y => y.MapFrom(t => $"{t.Day.Month.ToString("00")}-{t.Day.Day} {t.Day.ToString("dddd", new CultureInfo("hu-HU"))}"));

            CreateMap<CarouselItem, CarouselItemDto>()
                .ForMember(q => q.BackgroundPath, y => y.MapFrom(t => t.BackgroundImage.LocalCrops));

            CreateMap<GaleryPicture, PictureDto>()
                .ForMember(q => q.ImageUrl, y => y.MapFrom(t => t.Image.LocalCrops));

            CreateMap<BlockGridItem, GridBlockDto>()
                .ForMember(q => q.LeftText, y => y.Ignore())
                .ForMember(q => q.RightText, y => y.Ignore())
                .ForMember(q => q.OneColumnText, y => y.Ignore())
                .AfterMap((src, dsc) =>
                {
                    if (src.Content.ContentType.Alias == TwoColumnLayout.ModelTypeAlias)
                    {
                        var left = src.Areas.FirstOrDefault(x => x.Alias == "left");
                        var leftPublished = left.Select(x => x.Content).FirstOrDefault();
                        var leftContent = leftPublished as GridRichText;

                        var right = src.Areas.FirstOrDefault(x => x.Alias == "right");
                        var rightPublished = right?.Select(x => x.Content).FirstOrDefault();
                        var rightContent = rightPublished as GridRichText;

                        dsc.LeftText = leftContent?.RichText?.ToString();
                        dsc.RightText = rightContent?.RichText?.ToString();

                    }
                    else if (src.Content.ContentType.Alias == GridRichText.ModelTypeAlias)
                    {
                        var oneColumn = src.Content as GridRichText;
                        dsc.OneColumnText = oneColumn?.RichText?.ToString();
                    }
                });

            CreateMap<SocialLink, SocialLinkDto>()
                .ForMember(q => q.ImageUrl, y => y.MapFrom(t => t.Icon.LocalCrops));

            CreateMap<WeeklyGroup, WeeklyGroupDto>();
        }
    }
}
