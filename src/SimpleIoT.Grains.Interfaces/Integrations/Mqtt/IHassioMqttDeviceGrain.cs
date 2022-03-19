using Orleans;

namespace SimpleIoT.Grains.Interfaces.Integrations.Mqtt;

public interface IHassioMqttDeviceGrain : IGrainWithStringKey
{
    Task ConnectAsync(string topic);
    Task AddAttributeAsync(string deviceClass, string stateClass);
}