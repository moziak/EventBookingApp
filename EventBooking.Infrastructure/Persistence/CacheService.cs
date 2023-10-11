using System;
using System.Text.Json;
using EventBooking.Application.Common.Interfaces.Interfaces;
using EventBooking.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace EventBooking.Infrastructure.Persistence;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    private readonly InfrastructureSetting _infrastructureSetting;
    public CacheService(IOptions<InfrastructureSetting> infrastructureSetting)
    {
        _infrastructureSetting = infrastructureSetting.Value;
        var redis = ConnectionMultiplexer.Connect($"{_infrastructureSetting.RedisServer}:{_infrastructureSetting.RedisPort}");
        _cacheDb = redis.GetDatabase();
    }

    public async Task<T?> GetData<T>(string key)
    {
        var value = await _cacheDb.StringGetAsync(key);
        if (!string.IsNullOrEmpty(value))
        {

            return JsonSerializer.Deserialize<T>(value);
        }
        return default;
    }

    public async Task<object> RemoveData(string key)
    {
        var _exit = await _cacheDb.KeyExistsAsync(key);
        if (_exit)
        {
            return _cacheDb.KeyDeleteAsync(key);
        }
        return false;
    }

    public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return await _cacheDb.StringSetAsync(key, JsonSerializer.Serialize<T>(value));

    }
}


