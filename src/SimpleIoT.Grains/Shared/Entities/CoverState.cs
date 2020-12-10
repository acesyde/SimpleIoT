using System.ComponentModel.DataAnnotations;

namespace SimpleIoT.Grains.Shared.Entities
{
    public class CoverState
    {
        [Range(0, 100)]
        public int Position { get; set; }
        [Range(0, 100)]
        public int TiltPosition { get; set; }
        public bool IsOpening { get; set; }
        public bool IsClosing { get; set; }
        public bool IsClosed { get; set; }
    }
}
