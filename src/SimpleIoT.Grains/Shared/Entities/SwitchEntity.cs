using Orleans;
using SimpleIoT.Grains.Interfaces;

namespace SimpleIoT.Grains.Shared.Entities;

public class SwitchEntity : Grain<SwitchState>, IEntityGrain
{
}