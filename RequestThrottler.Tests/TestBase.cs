using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace RequestThrottler.Tests
{
    [TestFixture]
    public class TestBase
    {
        public IMemoryCache MemoryCache { get; set; }
        
        [SetUp]
        public async Task SetupApplicationHostEnvironment()
        {
            var hostBuilder = new HostBuilder().ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.Configure(app =>
                {
                    app.Run(handle => handle
                        .Response
                        .StartAsync());
                });

                webHost.ConfigureServices(config =>
                {
                    config.AddSingleton<IMemoryCache, MemoryCache>();
                });
            });

            var host = await hostBuilder.StartAsync();
            var serviceProvider = host.Services;
            
            
            MemoryCache = serviceProvider.GetService<IMemoryCache>();
        }
    }
}