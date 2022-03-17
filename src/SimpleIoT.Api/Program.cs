using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansDashboard;
using SimpleIoT.Grains;

var builder = WebApplication.CreateBuilder();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "SimpleIoT";
    });
    siloBuilder.Configure<EndpointOptions>(options => { options.AdvertisedIPAddress = IPAddress.Loopback; });
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

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleIoT.Api v1"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.UseOrleansDashboard(new DashboardOptions
{
    HideTrace = true,
    BasePath = "/dashboard"
});

app.Run();

public partial class Program {}