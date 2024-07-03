namespace ZWave.Models
{
    public class Ellipse : IShapeROI
    {
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float RadiusX { get; set; }
        public float RadiusY { get; set; }
        public float Phi { get; set; }
        public float? Score { get; set; }

        public bool ContainsPoint(Point point)
        {
            var centerPoint = GetCenterPoint();
            if (Phi == 0)
            {
                if (RadiusX != RadiusY)
                {
                    var distanceX = Math.Abs(point.X - centerPoint.X);
                    var distanceY = Math.Abs(point.Y - centerPoint.Y);

                    return (Math.Pow(distanceX, 2) / Math.Pow(RadiusX, 2) + Math.Pow(distanceY, 2) / Math.Pow(RadiusY, 2)) <= 1;
                }
                else 
                {
                    float distanceSquared = (float)Math.Pow(point.X - CenterX, 2) + (float)Math.Pow(point.Y - CenterY, 2);
                    return distanceSquared <= RadiusX * RadiusY;
                }
            }
            else
            {
                var rotationAngleInRadians = Math.PI * Phi / 180;

                var cosAngle = Math.Cos(rotationAngleInRadians);
                var sinAngle = Math.Sin(rotationAngleInRadians);

                var relativeX = point.X - centerPoint.X;
                var relativeY = point.Y - centerPoint.Y;

                var rotatedX = relativeX * cosAngle + relativeY * sinAngle;
                var rotatedY = -relativeX * sinAngle + relativeY * cosAngle;

                // Check if point is within the rotated ellipse equation
                var squaredTerm1 = Math.Pow(rotatedX, 2) / Math.Pow(RadiusX, 2);
                var squaredTerm2 = Math.Pow(rotatedY, 2) / Math.Pow(RadiusY, 2);

                return squaredTerm1 + squaredTerm2 <= 1; // Point is inside if sum <= 1
            }
        }

        public Point GetCenterPoint()
        {
            return new Point(CenterX, CenterY);
        }
    }
}
