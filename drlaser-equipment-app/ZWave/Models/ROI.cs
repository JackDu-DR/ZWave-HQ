using ZWave.Enums;

namespace ZWave.Models
{
    public class ROI
    {
        public ShapeType ShapeType { get; set; }
        public Coordinator Coordinator { get; set; }
        public double Phi { get; set; }
    }
}
