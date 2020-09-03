using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Infrastructure.Context.Configuration;
using Catalog.Infrastructure.Models;
using Catalog.Infrastructure.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Catalog.Infrastructure.Context
{
    // Add-Migration InitialCreate -Context CatalogDbContext
    // Update-Database -Context CatalogDbContext
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseSqlServer("Server=DESKTOP-NKFQK11;Database=TestMicro.CatalogDb;User Id = sa;Password = noname9xnd;MultipleActiveResultSets=true");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
