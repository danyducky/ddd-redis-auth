using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services;

public class RedisCache : IRedisCache
{
    private readonly IDistributedCache _cache;

    public RedisCache(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        var value = _cache.GetString(key);

        return value != null
            ? JsonSerializer.Deserialize<T>(value)
            : default;
    }

    public void Set<T>(string key, T value, Action<DistributedCacheEntryOptions> action)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };

        action(options);

        _cache.SetString(key, JsonSerializer.Serialize(value), options);
    }

    public void Clear(string key)
    {
        _cache.Remove(key);
    }
}