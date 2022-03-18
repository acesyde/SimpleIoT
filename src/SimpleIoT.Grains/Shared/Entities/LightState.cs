using System.ComponentModel.DataAnnotations;

namespace SimpleIoT.Grains.Shared.Entities;

public class LightState
{
    [Range(0, 255)]
    public int Brightness { get; set; }

    public int ColorTemperature { get; set; }

    public bool IsOn { get; set; }

    public int MaxMireds { get; set; }

    public int MinMireds { get; set; }
}