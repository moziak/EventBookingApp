using System;
using EventBooking.Application.Common.Interfaces.Persistence;

namespace EventBooking.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly EventBookingDbContext _context;
    private bool _disposed;

    public UnitOfWork(EventBookingDbContext context)
    {
        _context = context;
    }

    public void Dispose() => this.Dispose(true);

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}


