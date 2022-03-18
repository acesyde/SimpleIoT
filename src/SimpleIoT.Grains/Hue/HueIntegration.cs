using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using SimpleIoT.Grains.Interfaces;
using SimpleIoT.Grains.Shared;

namespace SimpleIoT.Grains.Hue;

public sealed class HueIntegration : IntegrationGrain
{
    public Dictionary<Guid, IDeviceGrain> Bridges { get; } = new Dictionary<Guid, IDeviceGrain>();

    public override Task InitiazeAsync()
    {
        return Task.CompletedTask;
    }

    public override Task AddDeviceAsync(IDeviceGrain device)
    {
        var key = device.GetPrimaryKey();

        if (!Bridges.ContainsKey(key))
        {
            Bridges.Add(key, device);
        }

        return Task.CompletedTask;
    }

    public override Task<string[]> GetDevicesAsync()
    {
        return Task.FromResult(Bridges.Values.Select(m => m.GetPrimaryKey().ToString()).ToArray());
    }
}