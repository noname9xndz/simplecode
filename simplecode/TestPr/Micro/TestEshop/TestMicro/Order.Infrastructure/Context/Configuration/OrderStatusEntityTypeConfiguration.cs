using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Infrastructure.Domain.AggregatesModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Context.Configuration
{
    public class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> orderStatusConfiguration)
        {
            orderStatusConfiguration.ToTable("orderstatus", OrderDbContext.DEFAULT_SCHEMA);

            orderStatusConfiguration.HasKey(o => o.Id);

            orderStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            orderStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
