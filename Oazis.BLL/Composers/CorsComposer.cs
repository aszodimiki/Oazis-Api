using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Oazis.BLL.Composers
{
    public class CorsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowOazis",
            //        policy =>
            //        {
            //            policy.AllowAnyOrigin()
            //                .AllowAnyHeader()
            //                .AllowAnyMethod(); // add the allowed origins  
            //        });
            //});
        }
    }
}
