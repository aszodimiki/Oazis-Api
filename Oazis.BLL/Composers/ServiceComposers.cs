using Microsoft.Extensions.DependencyInjection;
using Oazis.BLL.Services;
using Oazis.BLL.Services.Interfaces;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Oazis.BLL.Composers
{
    internal class ServiceComposers : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<ISiteService, SiteService>();
            builder.Services.AddScoped<IDetailService, DetailService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IInformationsService, InformationsService>();
            builder.Services.AddScoped<IWeeklyService, WeeklyService>();
            builder.Services.AddScoped<ITextsService, TextsService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped(typeof(IGenericFoodService<>), typeof(GenericFoodService<>));
        }
    }
}
