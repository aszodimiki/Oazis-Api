using Microsoft.EntityFrameworkCore;

namespace Oazis.Domain.Data
{
    public class OazisContext : DataContext
    {
        public OazisContext(DbContextOptions<OazisContext> options) : base(options)
        {
            
        }
    }
}
