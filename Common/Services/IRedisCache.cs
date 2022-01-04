﻿using Microsoft.Extensions.Caching.Distributed;

namespace Common.Services
{
    public interface IRedisCache
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value, DistributedCacheEntryOptions? options = null);
        void Clear(string key);
    }
}