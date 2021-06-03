using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NExpect;
using NSubstitute;
using NUnit.Framework;

namespace RequestThrottler.Tests
{
    [TestFixture]
    public class ThrottleTests : TestBase
    {
        [TestFixture]
        public class Behaviour
        {
            [Test]
            public void AssertThatLinearThrottlingDoesThrottleForOneSecondInterval()
            {
                // Arrange
                var throttle = new Throttle(Policy.Linear, new ThrottleArguments
                {
                    PersistIpBan = true,
                    TimeInterval = TimeInterval.OneHour
                });
                var context = ContextBuilder.BuildActionExecutingContext();
                // Act
                throttle.OnActionExecuting(context);
                // Assert
            }
        }
    }
}