using EventLogEF.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLogEF.Context
{
    // Add-Migration Initial_EventLogDbContext -Context EventLogDbContext
    // Update-Database -Context EventLogDbContext
    public class EventLogDbContext : DbContext
    {
        public EventLogDbContext(DbContextOptions<EventLogDbContext> options) : base(options)
        {
        }

        public DbSet<EventLogEntry> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<EventLogEntry> builder)
        {
            builder.ToTable("EventLogs");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.EventTypeName)
                .IsRequired();
        }
    }
}