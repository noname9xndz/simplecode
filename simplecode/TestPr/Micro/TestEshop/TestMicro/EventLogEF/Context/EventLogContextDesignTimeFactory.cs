using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventLogEF.Context
{
    public class EventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<EventLogDbContext>
    {
        public EventLogDbContext CreateDbContext(string[] args)
        {
            //var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            //                                              .AddJsonFile("appsettings.json")
            //                                              .AddEnvironmentVariables()
            //                                              .Build();

            //var optionsBuilder = new DbContextOptionsBuilder<EventLogDbContext>();
            //var connectionString = configuration.GetConnectionString("ConnectionString");
            //optionsBuilder.UseSqlServer(connectionString,
            //optionsBuilder.UseSqlServer(".",

            var optionsBuilder = new DbContextOptionsBuilder<EventLogDbContext>();
            //optionsBuilder.UseSqlServer("Server=DESKTOP-QRPI657;Database=TestMicro.CatalogDb;User Id = sa;Password = noname9xnd;MultipleActiveResultSets=true",
            optionsBuilder.UseSqlServer("Server=DESKTOP-QRPI657;Database=TestMicro.OrderDb;User Id = sa;Password = noname9xnd;MultipleActiveResultSets=true",
                    options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new EventLogDbContext(optionsBuilder.Options);
        }
    }
}