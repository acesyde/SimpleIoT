using Orleans;
using Orleans.Runtime;
using SimpleIoT.Grains.Interfaces;
using SimpleIoT.Grains.Interfaces.Integrations.Mqtt;

public class MqttStartupGrain : IStartupTask
{
    private readonly IGrainFactory _grainFactory;

    public MqttStartupGrain(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
    }
    
    public async Task Execute(CancellationToken cancellationToken)
    {
        var mqttGrain = _grainFactory.GetGrain<IHassioMqttIntegration>(0);
        await mqttGrain.ConnectAsync();
    }
}