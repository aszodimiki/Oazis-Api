using Microsoft.EntityFrameworkCore;

namespace Oazis.Domain.Data
{
    public abstract class DataContext : DbContext
    {
        protected DataContext(DbContextOptions options) : base(options)
        { }
    }
}
