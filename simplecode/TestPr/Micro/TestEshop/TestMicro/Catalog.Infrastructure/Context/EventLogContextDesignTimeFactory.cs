using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Catalog.Infrastructure.Context
{
    public class EventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<EventLogDbContext>
    {
        public EventLogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventLogDbContext>();

            optionsBuilder.UseSqlServer(".",
                options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new EventLogDbContext(optionsBuilder.Options);
        }
    }
}
