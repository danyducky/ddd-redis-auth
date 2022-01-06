using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services;

public interface IRedisCache
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, Action<DistributedCacheEntryOptions> action);
    void Clear(string key);
}