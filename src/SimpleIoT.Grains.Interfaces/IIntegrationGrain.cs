using System.Threading.Tasks;
using Orleans;

namespace SimpleIoT.Grains.Interfaces
{
    public interface IIntegrationGrain : IGrainWithGuidKey
    {
        Task InitiazeAsync();

        Task AddDeviceAsync(IDeviceGrain device);

        Task<string[]> GetDevicesAsync();
    }
}
