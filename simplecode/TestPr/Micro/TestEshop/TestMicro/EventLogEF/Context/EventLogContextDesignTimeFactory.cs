using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventLogEF.Context
{
    public class EventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<EventLogDbContext>
    {
        public EventLogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventLogDbContext>();

            //optionsBuilder.UseSqlServer(".",
            optionsBuilder.UseSqlServer("Server=DESKTOP-QRPI657;Database=TestMicro.CatalogDb;User Id = sa;Password = noname9xnd;MultipleActiveResultSets=true",
                options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new EventLogDbContext(optionsBuilder.Options);
        }
    }
}
