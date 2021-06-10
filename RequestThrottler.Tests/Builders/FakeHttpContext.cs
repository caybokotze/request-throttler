using Microsoft.AspNetCore.Http;

namespace RequestThrottler.Tests.Builders
{
    public class FakeHttpContext
    {
        public static HttpContext Create()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["request"] = "";
            return httpContext;
        }
    }
}