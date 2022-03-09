using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Redis.Caching.Services
{
    public class ResponceCacheService : IResponceCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponceCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object responce, TimeSpan timeToLive)
        {
            if(responce == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(responce);

            await _distributedCache.SetStringAsync( cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive,
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync( cacheKey );
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
