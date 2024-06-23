namespace Oazis.Domain.Models
{
    public class WeeklyGroupDto
    {
        public string WeeklyTitle { get; set; }
        public List<DailyMenuDto> DailyMenus { get; set; }
    }
}
