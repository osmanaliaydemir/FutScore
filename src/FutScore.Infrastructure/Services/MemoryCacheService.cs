using System;
using System.Threading.Tasks;
using FutScore.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FutScore.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            if (_cache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }

            var value = await factory();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(expiration ?? TimeSpan.FromMinutes(30));

            _cache.Set(key, value, cacheEntryOptions);
            return value;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            // MemoryCache doesn't support pattern-based removal
            // This is a limitation of the in-memory cache
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(_cache.TryGetValue(key, out _));
        }
    }
} 