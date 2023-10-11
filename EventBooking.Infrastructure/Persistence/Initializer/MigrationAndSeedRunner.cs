using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventBooking.Infrastructure.Persistence.Initializer;

public class MigrationAndSeedRunner
{
    private readonly EventBookingDbContext _dbContext;
    private readonly ILogger<MigrationAndSeedRunner> _logger;
    public MigrationAndSeedRunner(EventBookingDbContext dbContext, ILogger<MigrationAndSeedRunner> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        if (_dbContext.Database.GetMigrations().Any())
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Applying Migrations ");
                await _dbContext.Database.MigrateAsync();
            }
        }
    }
}

