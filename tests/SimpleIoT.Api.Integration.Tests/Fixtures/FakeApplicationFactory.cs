using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using SimpleIoT.Grains;

namespace SimpleIoT.Api.Integration.Tests.Fixtures
{
    public class FakeApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseStartup<TStartup>();
                })
                .UseOrleans(builder =>
                {
                    builder.UseLocalhostClustering();
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "SimpleIoT";
                    });
                    builder.Configure<EndpointOptions>(options =>
                    {
                        options.AdvertisedIPAddress = IPAddress.Loopback;
                    });
                    builder.UseInMemoryReminderService();
                    builder.AddSimpleMessageStreamProvider("sms");
                    builder.ConfigureApplicationParts(manager =>
                    {
                        manager.AddApplicationPart(GrainModule.Assembly).WithReferences();
                    });
                    builder.ConfigureServices((context, collection) =>
                    {

                    });
                    builder.ConfigureLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddConsole();
                    });
                    builder.UseDashboard(options =>
                    {
                        options.HideTrace = true;
                        options.HostSelf = false;
                    });
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }

        protected override void Dispose(bool disposing)
        {
            Server?.Dispose();
        }
    }
}
