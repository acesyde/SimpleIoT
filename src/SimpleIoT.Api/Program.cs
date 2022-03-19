using System.Net;
using FastEndpoints;
using FastEndpoints.Swagger;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansDashboard;
using SimpleIoT.Grains.Integrations.Mqtt.Configuration;

var builder = WebApplication.CreateBuilder();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "SimpleIoT";
    });
    siloBuilder.AddStartupTask<MqttStartupGrain>();
    siloBuilder.Configure<EndpointOptions>(options => { options.AdvertisedIPAddress = IPAddress.Loopback; });
    siloBuilder.UseInMemoryReminderService();
    siloBuilder.UseDashboard(options => { });
});

builder.Services.Configure<MqttConfiguration>(builder.Configuration.GetSection("mqtt"));
builder.Services.AddHealthChecks();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Map("/dashboard", d =>
{
    d.UseOrleansDashboard(new DashboardOptions
    {
        HideTrace = true
    });
});
app.MapHealthChecks("/health");

app.Run();

public partial class Program {}