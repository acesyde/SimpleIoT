using System.Text.Json;
using System.Text.Json.Nodes;
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

public class HassioMqttDeviceGrain : Grain, IHassioMqttDeviceGrain
{
    private readonly IOptions<MqttConfiguration> _options;
    private readonly ILogger<HassioMqttDeviceGrain> _logger;
    private readonly IMqttClient _mqttClient;
    private readonly MqttFactory _mqttFactory;
    private string _topicToSubscribe;
    private readonly IDictionary<string, string> _attributes = new Dictionary<string, string>();
    private readonly IDictionary<string, string> _values = new Dictionary<string, string>();

    public HassioMqttDeviceGrain(IOptions<MqttConfiguration> options, ILogger<HassioMqttDeviceGrain> logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mqttFactory = new MqttFactory();
        _mqttClient = _mqttFactory.CreateMqttClient();
    }

    public async Task ConnectAsync(string topic)
    {
        _topicToSubscribe = topic;

        if (!_mqttClient.IsConnected)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_options.Value.Url, _options.Value.Port)
                .WithProtocolVersion(MqttProtocolVersion.V311)
                .WithCommunicationTimeout(TimeSpan.FromSeconds(5))
                .Build();

            _mqttClient.UseConnectedHandler(ConnectionHandler);
            await _mqttClient.ConnectAsync(mqttClientOptions);
        }
    }

    public Task AddAttributeAsync(string deviceClass, string stateClass)
    {
        if (_attributes.ContainsKey(deviceClass))
            return Task.CompletedTask;

        _attributes.Add(deviceClass, stateClass);
        return Task.CompletedTask;
    }

    private async Task ConnectionHandler(MqttClientConnectedEventArgs arg)
    {
        _mqttClient.UseApplicationMessageReceivedHandler(HandleDeviceMessage);

        var mqttSubscribeOptions = _mqttFactory
            .CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => { f.WithTopic(_topicToSubscribe); })
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions);
    }

    private Task HandleDeviceMessage(MqttApplicationMessageReceivedEventArgs arg)
    {
        var jsonDocument = JsonNode.Parse(arg.ApplicationMessage.Payload);
        if (jsonDocument == null)
            return Task.CompletedTask;

        if (_attributes.ContainsKey("battery"))
        {
            if (jsonDocument["battery"] != null)
            {
                _values["battery"] = jsonDocument["battery"].ToString();
            }
        }

        if (_attributes.ContainsKey("occupancy"))
        {
            if (jsonDocument["occupancy"] != null)
            {
                _values["occupancy"] = jsonDocument["occupancy"].ToString();
            }
        }

        if (_attributes.ContainsKey("contact"))
        {
            if (jsonDocument["contact"] != null)
            {
                _values["contact"] = jsonDocument["contact"].ToString();
            }
        }

        _logger.LogInformation($"Handle new message from : {arg.ApplicationMessage.Topic}");
        return Task.CompletedTask;
    }
}