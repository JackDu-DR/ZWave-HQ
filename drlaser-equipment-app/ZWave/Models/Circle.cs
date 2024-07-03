namespace ZWave.Models
{
    public class Circle : IShapeROI
    {
        public float Radius { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Phi { get; set; }
        public float? Score { get; set; }

        public bool ContainsPoint(Point point)
        {
            float distanceSquared = (float)Math.Pow(point.X - CenterX, 2) + (float)Math.Pow(point.Y - CenterY, 2);
            return distanceSquared <= Radius * Radius;
        }

        public Point GetCenterPoint()
        {
            return new Point(CenterX, CenterY);
        }
    }
}
