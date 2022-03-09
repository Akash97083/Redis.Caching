using System;
using System.Threading.Tasks;

namespace Redis.Caching.Services
{
    public interface IResponceCacheService
    {
        Task CacheResponseAsync(string cacheKey, object responce, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
         
    }
}
