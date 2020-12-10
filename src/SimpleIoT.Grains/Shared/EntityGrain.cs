using System;
using Microsoft.Extensions.Logging;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared
{
    public abstract class EntityGrain<TState> : IEntityGrain
        where TState : State
    {
        private readonly ILogger<EntityGrain<TState>> _logger;

        public string Name { get; }

        protected EntityGrain(ILogger<EntityGrain<TState>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
