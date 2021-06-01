using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NExpect;
using NUnit.Framework;

namespace RequestThrottler.Tests
{
    [TestFixture]
    public class ThrottleTests
    {
        [TestFixture]
        public class Behaviour
        {
            public void AssertThatLinearThrottlingDoesThrottleForOneSecondInterval()
            {
                var throttle = new Throttle(Policy.Linear, TimeInterval.OneSecond);
                var actionContext = new ActionContext();
                var filterList = new List<IFilterMetadata>();
                var actionArguments = new Dictionary<string, object>();
                var controller = new object();
                var context = new ActionExecutingContext(
                    actionContext, 
                    filterList, 
                    actionArguments, 
                    controller);
                
                throttle.OnActionExecuting(context);
            }
        }
    }
}