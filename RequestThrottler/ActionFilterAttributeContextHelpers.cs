using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace RequestThrottler
{
    public static class ActionFilterAttributeContextHelpers
    {
        public static IMemoryCache GetMemoryCache(this ActionExecutingContext context)
        {
            var service = (IMemoryCache) context
                .HttpContext
                .RequestServices
                .GetService(typeof(IMemoryCache));

            if (service is null)
            {
                throw new ServiceNotImplementedException();
            }

            return service;
        }
        
        public static void ResetCacheValues(
            this IMemoryCache cache,
            string ipAddress,
            CachedValues cachedValues)
        {
            cache.Remove(ipAddress);
            cache.Set(ipAddress, cachedValues);
        }
        
        public static string GetRemoteIpAddress(this ActionExecutingContext context)
        {
            return context
                .HttpContext
                .Connection
                .RemoteIpAddress
                .ToString();
        }
    }
}