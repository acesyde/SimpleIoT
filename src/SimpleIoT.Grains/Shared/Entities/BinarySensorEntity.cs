using Orleans;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared.Entities;

public class BinarySensorEntity : Grain<BinarySensorState>, IEntityGrain
{
}