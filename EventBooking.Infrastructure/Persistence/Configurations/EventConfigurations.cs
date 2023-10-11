using System;
using EventBooking.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Infrastructure.Persistence.Configurations
{
    public class EventConfigurations : IEntityTypeConfiguration<Event>
    {

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            ConfigureEventsTable(builder);
        }

        private void ConfigureEventsTable(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).HasMaxLength(100);
            builder.Property(e => e.Description);
            builder.Property(e => e.UserId);
            builder.OwnsOne(e => e.Duration, db =>
            {
                db.Property(d => d.EndDate).HasColumnName("EndDate");
                db.Property(d => d.StartDate).HasColumnName("StartDate");
                db.Property(d => d.TimeZone).HasColumnName("TimeZone");
            });
            builder.OwnsOne(e => e.Address, ab =>
            {
                ab.Property(d => d.Country).HasColumnName("Country");
                ab.Property(d => d.Number).HasColumnName("Number");
                ab.Property(d => d.State).HasColumnName("State");
                ab.Property(d => d.Street).HasColumnName("Street");
            });

        }
    }
}

