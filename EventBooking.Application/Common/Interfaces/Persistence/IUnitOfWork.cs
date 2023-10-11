using System;
namespace EventBooking.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task<int> Save();
}

