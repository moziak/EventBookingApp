using System;
using EventBooking.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence;

public class EventBookingDbContext : DbContext
{
    public EventBookingDbContext(DbContextOptions<EventBookingDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventBookingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}


