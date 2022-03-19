using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Formatter;
using Orleans;
using SimpleIoT.Grains.Integrations.Mqtt.Configuration;
using SimpleIoT.Grains.Interfaces.Integrations.Mqtt;

namespace SimpleIoT.Grains.Integrations.Mqtt.Grains;

public class HassioMqttIntegration : Grain, IHassioMqttIntegration
{
    private readonly IOptions<MqttConfiguration> _options;
    private readonly ILogger<HassioMqttIntegration> _logger;
    private readonly IGrainFactory _grainFactory;
    private readonly IMqttClient _mqttClient;
    private readonly MqttFactory _mqttFactory;
    private readonly IDictionary<string, IHassioMqttDeviceGrain> _deviceGrains = new Dictionary<string, IHassioMqttDeviceGrain>();

    public HassioMqttIntegration(IOptions<MqttConfiguration> options, ILogger<HassioMqttIntegration> logger, IGrainFactory grainFactory)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _mqttFactory = new MqttFactory();
        _mqttClient = _mqttFactory.CreateMqttClient();
    }

    public async Task ConnectAsync()
    {
        if (!_mqttClient.IsConnected)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_options.Value.Url, _options.Value.Port)
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .WithCommunicationTimeout(TimeSpan.FromSeconds(5))
                .Build();

            _mqttClient.UseConnectedHandler(Handler);
            await _mqttClient.ConnectAsync(mqttClientOptions);
        }
    }

    private async Task Handler(MqttClientConnectedEventArgs arg)
    {
        _mqttClient.UseApplicationMessageReceivedHandler(HandleDiscoveryTopic);

        var mqttSubscribeOptions = _mqttFactory
            .CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => { f.WithTopic("homeassistant/#"); })
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions);
    }

    private async Task HandleDiscoveryTopic(MqttApplicationMessageReceivedEventArgs arg)
    {
        if (string.IsNullOrWhiteSpace(arg.ApplicationMessage?.Topic))
        {
            return;
        }

        var match = Regex.Match(arg.ApplicationMessage.Topic, "^[a-z]*/([a-z_]*)/([a-zA-Z0-9_-]*)/([a-z]*)/config$");
        if (!match.Success)
        {
            return;
        }

        string group = match.Groups[1].Value;
        string identifier = match.Groups[2].Value;
        string entityType = match.Groups[3].Value;

        var hassioMqttConfig = JsonSerializer.Deserialize<HassioMqttConfig>(arg.ApplicationMessage.Payload);

        if (hassioMqttConfig == null)
            return;

        if (!_deviceGrains.ContainsKey(identifier))
        {
            var hassioMqttDeviceGrain = _grainFactory.GetGrain<IHassioMqttDeviceGrain>(identifier);
            await hassioMqttDeviceGrain.ConnectAsync(hassioMqttConfig.StateTopic);
            _deviceGrains.Add(identifier, hassioMqttDeviceGrain);
        }
        
        await _deviceGrains[identifier].AddAttributeAsync(hassioMqttConfig.DeviceClass, hassioMqttConfig.StateClass);
    }
}

internal class HassioMqttConfig
{
    [JsonPropertyName("state_topic")]
    public string StateTopic { get; set; }
    
    [JsonPropertyName("device_class")]
    public string DeviceClass { get; set; }
    
    [JsonPropertyName("state_class")]
    public string StateClass { get; set; }
}