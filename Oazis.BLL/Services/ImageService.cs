using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public ImageService(IPublishedValueFallback publishedValueFallback, ISiteService siteService, IMapper mapper)
        {
            _publishedValueFallback = publishedValueFallback;
            _siteService = siteService;
            _mapper = mapper;
        }

        public async Task<List<PictureDto>> GetPictures()
        {
            var rootSite = await _siteService.GetRootSite();
            var galleryRoot = rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == Galery.ModelTypeAlias);

            var pictures = galleryRoot.Children.Where(x => x.ContentType.Alias == GaleryPicture.ModelTypeAlias).ToList();

            var result = pictures.Select(x => _mapper.Map<PictureDto>(x)).ToList();

            return result;
        }
    }
}
