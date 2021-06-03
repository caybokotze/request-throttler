using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestThrottler.Tests
{
    public class ContextBuilder
    {
        public static ActionExecutingContext BuildActionExecutingContext()
        {
            var actionContext = new ActionContext();
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