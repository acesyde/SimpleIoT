using Microsoft.Extensions.Logging;

namespace SimpleIoT.Grains.Shared.Entities
{
    public class BinarySensorEntity : EntityGrain<BinarySensorState>
    {
        public BinarySensorEntity(ILogger<BinarySensorEntity> logger) : base(logger)
        {
        }
    }
}
