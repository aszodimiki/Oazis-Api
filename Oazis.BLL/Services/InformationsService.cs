using AutoMapper;
using Oazis.BLL.Services.Interfaces;
using Oazis.Domain.Models;
using Oazis.Domain.ModelsBuilder;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Oazis.BLL.Services
{
    public class InformationsService : IInformationsService
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public InformationsService(IPublishedValueFallback publishedValueFallback, ISiteService siteService, IMapper mapper)
        {
            _publishedValueFallback = publishedValueFallback;
            _siteService = siteService;
            _mapper = mapper;
        }

        public async Task<InformationsDTO> GetAllInformation()
        {
            var informationRootContent = await _siteService.GetRootSite();
            var informationsContent =
                informationRootContent.Children.FirstOrDefault(x => x.ContentType.Alias == Information.ModelTypeAlias);
            var result = _mapper.Map<InformationsDTO>(new Information(informationsContent, _publishedValueFallback));

            return result;
        }

        public async Task<FooterTextsDTO> GetFooterTexts()
        {
            var rootSite = await _siteService.GetRootSite();
            var footerContent =
                rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == FooterTexts.ModelTypeAlias);
            var socialLinksSite =
                rootSite.Children.FirstOrDefault(x => x.ContentType.Alias == SocialLinks.ModelTypeAlias);
            var socialLinks = socialLinksSite.Children.Select(x => new SocialLink(x, _publishedValueFallback));
            var socialLinkDtos = socialLinks.Select(x => _mapper.Map<SocialLinkDto>(x)).ToList();

            var result = _mapper.Map<FooterTextsDTO>(new FooterTexts(footerContent, _publishedValueFallback));
            result.SocialLinks = socialLinkDtos;

            return result;
        }
    }
}
