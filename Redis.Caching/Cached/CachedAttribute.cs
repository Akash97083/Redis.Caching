using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Redis.Caching.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Caching.Cached
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {

            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before: check is request is cached
            // if true return
            var cachedKey = GenerateCachedKeyFromRequest(context.HttpContext.Request);

            var _cachingService = context.HttpContext.RequestServices.GetRequiredService<IResponceCacheService>();

            var cachedResponse = await _cachingService.GetCachedResponseAsync(cachedKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 203
                };
                context.Result = contentResult;
                return;
            }
            //after: get the value
            //cache the response
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
                await _cachingService.CacheResponseAsync(cachedKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
        }

        private static string GenerateCachedKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
