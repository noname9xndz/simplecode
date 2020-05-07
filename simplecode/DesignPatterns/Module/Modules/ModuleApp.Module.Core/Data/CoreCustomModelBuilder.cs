using Microsoft.EntityFrameworkCore;
using ModuleApp.Infrastructure.Data;
using ModuleApp.Module.Core.Models.MVC;

namespace ModuleApp.Module.Core.Data
{
    public class CoreCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<AppSetting>().ToTable("Core_AppSetting");

            modelBuilder.Entity<Entity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EntityId);
            });

            CoreSeedData.SeedData(modelBuilder);
        }
    }
}