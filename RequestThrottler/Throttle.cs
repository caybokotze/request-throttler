using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace RequestThrottler
{
    public class ThrottleArguments
    {
        public TimeInterval TimeInterval { get; set; }
        public bool PersistIpBan { get; set; }
    }
    
    public class Throttle : ActionFilterAttribute
    {
        private readonly Policy _policy;
        private readonly ThrottleArguments _throttleArguments;

        public Throttle(
            Policy policy, 
            ThrottleArguments throttleArguments = null)
        {
            _policy = policy;
            _throttleArguments = throttleArguments;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cache = context.GetMemoryCache();
            var ipAddress = context.GetRemoteIpAddress();
            var exists = false;
            
            if (cache.TryGetValue(ipAddress, out CachedValues cachedValues))
            {
                exists = true;

                if (cachedValues.SavedDateTime < DateTime.UtcNow)
                {
                    cache.ResetCacheValues(ipAddress, new CachedValues
                    {
                        SavedDateTime = CalculateWhenRequestShouldBeValid(
                            _policy, 
                            _throttleArguments.TimeInterval)
                    });
                    return;
                }

                if (!context.HttpContext.Response.HasStarted)
                {
                    throw new RequestNotAllowedException($"You need to wait {(cachedValues.SavedDateTime-DateTime.UtcNow).Seconds} seconds before you try again.");
                }
            }
            
            if (!exists)
            {
                var date = CalculateWhenRequestShouldBeValid(
                    _policy, 
                    _throttleArguments.TimeInterval);

                cache.Set(ipAddress, new CachedValues
                {
                    SavedDateTime = date
                });
            }
        }
        
        public DateTime CalculateWhenRequestShouldBeValid( 
            Policy policy, 
            TimeInterval timeInterval)
        {
            if (policy == Policy.Linear)
            {
                switch (timeInterval)
                {
                    case TimeInterval.OneSecond:
                        return DateTime.UtcNow.AddSeconds(1);
                    case TimeInterval.TwoSeconds:
                        return DateTime.UtcNow.AddSeconds(2);
                    case TimeInterval.TenSeconds:
                        return DateTime.UtcNow.AddSeconds(10);
                    case TimeInterval.OneMinute:
                        return DateTime.UtcNow.AddMinutes(1);
                    case TimeInterval.FiveMinutes:
                        return DateTime.UtcNow.AddMinutes(5);
                    case TimeInterval.OneHour:
                        return DateTime.UtcNow.AddHours(1);
                    case TimeInterval.TenMinutes:
                        return DateTime.UtcNow.AddMinutes(10);
                    case TimeInterval.OneDay:
                        return DateTime.UtcNow.AddDays(1);
                    case TimeInterval.OneWeek:
                        return DateTime.UtcNow.AddDays(7);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(timeInterval), timeInterval, null);
                }
            }

            return DateTime.UtcNow;
        }
    }
}