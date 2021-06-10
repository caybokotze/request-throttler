using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using NUnit.Framework;
using RequestThrottler.Tests.Builders;

namespace RequestThrottler.Tests
{
    [TestFixture]
    public class ThrottleTests : TestBase
    {
        [TestFixture]
        public class Behaviour : TestBase
        {
            [Test]
            public void AssertThatLinearThrottlingDoesThrottleForOneSecondInterval()
            {
                // Arrange
                var substitute = Substitute.For<Throttle>(Policy.Linear, TimeInterval.FiveMinutes, false);
                var context = new ActionExecutingContextBuilder(ServiceProvider).Build();
                // Act
                var throttler = new Throttle(Policy.Ban, TimeInterval.OneDay, true);
                throttler.OnActionExecuting(context);
                // Assert
                substitute.Received().OnActionExecuting(context);
            }
        }

        [ApiController]
        public class TestController
        {
            [Throttle(Policy.Ban)]
            public void DoThings()
            {
            }
        }

        public class ActionExecutingContextBuilder
        {
            private readonly IServiceProvider _serviceProvider;

            public ActionExecutingContextBuilder(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public ActionExecutingContext Build()
            {
                var httpContext = FakeHttpContext.Create();
                httpContext.RequestServices = _serviceProvider;

                var routes = new Dictionary<string, string>()
                {
                    {"key", "value"}
                };
                var actionContext = new ActionContext()
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(new RouteValueDictionary(routes)),
                    ActionDescriptor = new ActionDescriptor()
                };
                var filterList = new List<IFilterMetadata>();
                var actionArguments = new Dictionary<string, object>();
                var controller = new object();
                var context = new ActionExecutingContext(
                    actionContext,
                    filterList,
                    actionArguments,
                    controller);
                return context;
            }
        }
    }
}