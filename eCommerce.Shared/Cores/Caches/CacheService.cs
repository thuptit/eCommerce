using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace eCommerce.Shared.Cores.Caches;

public class CacheService  : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public void Set<T>(string key, T value)
    {
        _distributedCache.Set(key,GetBytes(value));
    }

    public void Set<T>(string key, T value, DateTimeOffset expiredDate)
    {
        TimeSpan expiryTime = expiredDate.DateTime.Subtract(DateTime.Now);
        _distributedCache.Set(key,GetBytes(value),new DistributedCacheEntryOptions(){SlidingExpiration = expiryTime});
    }

    public async Task SetAsync<T>(string key, T value)
    {
        await _distributedCache.SetAsync(key,GetBytes(value));
    }

    public async Task SetAsync<T>(string key, T value, DateTimeOffset expiredDate)
    {
        TimeSpan expiryTime = expiredDate.DateTime.Subtract(DateTime.Now);
        await _distributedCache.SetAsync(key,GetBytes(value),new DistributedCacheEntryOptions(){SlidingExpiration = expiryTime});
    }

    public string Get(string key)
    {
        var value = _distributedCache.Get(key);
        if (value == null)
            return string.Empty;
        return Encoding.UTF8.GetString(value);
    }

    public async Task<string> GetAsync(string key)
    {
        var value = await _distributedCache.GetAsync(key);
        if (value == null)
            return string.Empty;
        return Encoding.UTF8.GetString(value);
    }

    public void DeleteKey(string key)
    {
        throw new NotImplementedException();
    }

    public Task DeleteKeyAsync(string key)
    {
        throw new NotImplementedException();
    }

    private byte[] GetBytes<T>(T value)
    {
        var type = typeof(T);
        if (type.BaseType == typeof(System.ValueType))
        {
            return Encoding.UTF8.GetBytes(value.ToString());
        }
        else if (type == typeof(string))
        {
            return Encoding.UTF8.GetBytes(value as string);
        }
        else
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
        }
    }
}