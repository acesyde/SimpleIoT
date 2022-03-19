using Orleans;

namespace SimpleIoT.Grains.Interfaces.Integrations.Mqtt;

public interface IHassioMqttIntegration : IGrainWithIntegerKey
{
    Task ConnectAsync();
}