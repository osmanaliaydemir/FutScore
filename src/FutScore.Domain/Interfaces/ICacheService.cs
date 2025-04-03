using System;
using System.Threading.Tasks;

namespace FutScore.Domain.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
        Task RemoveAsync(string key);
        Task RemoveByPatternAsync(string pattern);
        Task<bool> ExistsAsync(string key);
    }
} 