using System;
namespace EventBooking.Application.Common.Interfaces.Interfaces;

public interface ICacheService
{
    Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
    Task<T?> GetData<T>(string key);
    Task<object> RemoveData(string key);
}


