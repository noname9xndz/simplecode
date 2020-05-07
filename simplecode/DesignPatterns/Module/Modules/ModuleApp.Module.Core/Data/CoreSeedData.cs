using Microsoft.EntityFrameworkCore;
using ModuleApp.Module.Core.Models.MVC;

namespace ModuleApp.Module.Core.Data
{
    public static class CoreSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<EntityType>().HasData(
                new EntityType("Vendor") { AreaName = "Core", RoutingController = "Vendor", RoutingAction = "VendorDetail", IsMenuable = false }
            );
        }
    }
}