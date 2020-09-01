﻿using Equinox.Domain.Core.Events;
using Equinox.Infra.Data.Mappings.ModelsMap;
using Microsoft.EntityFrameworkCore;


namespace Equinox.Infra.Data.Context
{
    //update-database -Context EventStoreSqlContext
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) { }

        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}