using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NExpect;
using NUnit.Framework;
using static NExpect.Expectations;

namespace RequestThrottler.Tests
{
    [TestFixture]
    public class HostBuilderTests : TestBase
    {
        [Test]
        public void AssertThatHostBuilderRegisteredIMemoryCache()
        {
            var memoryCache = MemoryCache;
            Expect(memoryCache).To.Not.Be.Null();
        }
    }
}