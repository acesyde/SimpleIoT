using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace SimpleIoT.Grains.Interfaces
{
    public interface IIntegrationManagerGrain : IGrainWithStringKey
    {
        Task EnableIntegrationAsync(Guid id);
        Task DisableIntegrationAsync(Guid id);
        Task<IEnumerable<Guid>> GetAvailableIntegrationsAsync();
        Task<IEnumerable<Guid>> GetEnabledIntegrationsAsync();
        Task<IEnumerable<Guid>> GetDisabledIntegrationsAsync();
    }
}
