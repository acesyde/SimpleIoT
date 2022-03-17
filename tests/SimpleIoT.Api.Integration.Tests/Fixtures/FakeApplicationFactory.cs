using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using SimpleIoT.Grains;

namespace SimpleIoT.Api.Integration.Tests.Fixtures;

public sealed class FakeApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseContentRoot(".");
        builder.UseEnvironment("Development");
        builder.UseOrleans(siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering();
            siloBuilder.Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "SimpleIoT";
            });
            siloBuilder.Configure<EndpointOptions>(options =>
            {
                options.AdvertisedIPAddress = IPAddress.Loopback;
            });
            siloBuilder.UseInMemoryReminderService();
            siloBuilder.AddSimpleMessageStreamProvider("sms");
            siloBuilder.ConfigureApplicationParts(manager =>
            {
                manager.AddApplicationPart(GrainModule.Assembly).WithReferences();
            });
            siloBuilder.UseDashboard(options =>
            {
                options.HideTrace = true;
                options.HostSelf = false;
            });
        });
        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        Server?.Dispose();
    }
}