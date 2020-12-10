namespace SimpleIoT.Grains.Shared.Entities
{
    public class SwitchState
    {
        public bool IsOn { get; set; }
        public float CurrentPower { get; set; }
        public float TotalEnergy { get; set; }
        public bool IsStandby { get; set; }
    }
}
