using Orleans;
using SimpleIoT.Grains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SimpleIoT.Grains.Managers
{
    public sealed class IntegrationManager : Grain, IIntegrationManagerGrain
    {
        private readonly ILogger<IntegrationManager> _logger;

        public Dictionary<Guid, IIntegrationGrain> AvailableIntegrations { get; } = new Dictionary<Guid, IIntegrationGrain>();

        public Dictionary<Guid, IIntegrationGrain> EnabledIntegrations { get; } = new Dictionary<Guid, IIntegrationGrain>();

        public Dictionary<Guid, IIntegrationGrain> DisabledIntegrations { get; } = new Dictionary<Guid, IIntegrationGrain>();

        public IntegrationManager(ILogger<IntegrationManager> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task EnableIntegrationAsync(Guid id)
        {
            _logger.LogInformation($"Enable integration `{id}`");

            if (DisabledIntegrations.TryGetValue(id, out var integration))
            {
                await integration.EnableAsync();

                EnabledIntegrations.Add(id, integration);
                DisabledIntegrations.Remove(id);
            }
        }

        public async Task DisableIntegrationAsync(Guid id)
        {
            _logger.LogInformation($"Disable integration `{id}`");

            if (EnabledIntegrations.TryGetValue(id, out var integration))
            {
                await integration.DisableAsync();

                
                DisabledIntegrations.Add(id, integration);
                EnabledIntegrations.Remove(id);
            }
        }

        public Task<IEnumerable<Guid>> GetAvailableIntegrationsAsync()
        {
            return Task.FromResult(AvailableIntegrations.Select(m => m.Key));
        }

        public Task<IEnumerable<Guid>> GetEnabledIntegrationsAsync()
        {
            return Task.FromResult(EnabledIntegrations.Select(m => m.Key));
        }

        public Task<IEnumerable<Guid>> GetDisabledIntegrationsAsync()
        {
            return Task.FromResult(DisabledIntegrations.Select(m => m.Key));
        }
    }
}
