using System.Collections.Generic;

namespace Oazis.Domain.Models
{
    public class WeeklyMenuDto
    {
        public string Information { get; set; }
        public List<WeeklyGroupDto> WeeklyGroups { get; set; } = new();
    }
}
