using Orleans;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared.Entities;

public class LightEntity : Grain<LightState>, IEntityGrain
{
}