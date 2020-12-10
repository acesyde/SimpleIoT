using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using SimpleIoT.Grains;

namespace SimpleIoT.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
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
}
