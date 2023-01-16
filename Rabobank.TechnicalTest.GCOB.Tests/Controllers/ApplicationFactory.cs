using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Rabobank.TechnicalTest.GCOB.Tests.Controllers
{
    public class ApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(hostBuilder => { hostBuilder.UseStartup<Startup>().UseTestServer(); })
                .ConfigureServices(services =>
                {
                    services.AddLogging();
                    services.AddSingleton<ILogger, FakeLogger>();
                })
                .ConfigureAppConfiguration((context, configurationBuilder) => { });
    }
}
