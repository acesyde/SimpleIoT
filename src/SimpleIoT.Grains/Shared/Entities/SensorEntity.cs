using Orleans;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared.Entities;

public class SensorEntity : Grain<SensorState>, IEntityGrain
{
}