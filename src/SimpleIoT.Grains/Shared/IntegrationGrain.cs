using System.Threading.Tasks;
using Orleans;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared;

public abstract class IntegrationGrain : Grain, IIntegrationGrain
{
    public abstract Task InitiazeAsync();
    public abstract Task AddDeviceAsync(IDeviceGrain device);
    public abstract Task<string[]> GetDevicesAsync();
}