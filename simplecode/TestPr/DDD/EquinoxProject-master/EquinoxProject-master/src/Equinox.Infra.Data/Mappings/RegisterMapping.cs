using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Infra.Data.Mappings.ModelsMap;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Mappings
{
    public static class RegisterMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
