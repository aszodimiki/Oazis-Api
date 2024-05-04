using System.Collections.Generic;

namespace Oazis.Domain.Models
{
    public class FooterTextsDTO
    {
        public string Address { get; set; }
        public string OpeningHours { get; set; }
        public List<SocialLinkDto> SocialLinks { get; set; }
    }
}
