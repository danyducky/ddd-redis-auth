using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Common.Services
{
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

        public void Set<T>(string key, T value, DistributedCacheEntryOptions? options = null)
        {
            _cache.SetString(key, JsonSerializer.Serialize(value), options ?? new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        }

        public void Clear(string key)
        {
            _cache.Remove(key);
        }
    }
}