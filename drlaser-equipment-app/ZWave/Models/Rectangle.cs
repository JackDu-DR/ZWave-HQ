namespace ZWave.Models
{
    public class Rectangle : IShapeROI
    {
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Phi { get; set; }
        public float? Score { get; set; }

        public bool ContainsPoint(Point point)
        {
            if (Phi == 0f)
            {
                var adjustingStartX = StartX;
                var adjustingStartY = StartY;
                var adjustingWidth = Width;
                var adjustingHeight = Height;
                if (Width < 0)
                {
                    adjustingStartX = StartX + Width;
                    adjustingWidth = Math.Abs(Width);
                }

                if (Height < 0)
                {
                    adjustingStartY = StartY + Height;
                    adjustingHeight = Math.Abs(Height);
                }

                var endX = adjustingStartX + adjustingWidth;
                var endY = adjustingStartY + adjustingHeight;

                return point.X >= adjustingStartX && point.X <= endX
                    && point.Y >= adjustingStartY && point.Y <= endY;
            }
            else
            {
                var centerPoint = GetCenterPoint();

                // Calculate half dimensions with rotation considered
                var halfWidth = Math.Abs(Width / 2f);
                var halfHeight = Math.Abs(Height / 2f);

                // Convert degrees to radians
                var radians = (float)(Phi * Math.PI / 180);

                // Calculate cosine and sine of rotation angle
                var cosAngle = (float)Math.Cos(radians);
                var sinAngle = (float)Math.Sin(radians);

                // Translate point relative to center
                var relativeX = point.X - centerPoint.X;
                var relativeY = point.Y - centerPoint.Y;

                // Apply inverse rotation to point (rotate point in opposite direction)
                var rotatedX = relativeX * cosAngle + relativeY * sinAngle;
                var rotatedY = -relativeX * sinAngle + relativeY * cosAngle;

                // Check if rotated point is within absolute rectangle boundaries
                return Math.Abs(rotatedX) <= halfWidth && Math.Abs(rotatedY) <= halfHeight;
            }
        }

        public Point GetCenterPoint()
        {
            var halfWidth = Math.Abs(Width / 2f);
            var halfHeight = Math.Abs(Height / 2f);
            if (Width < 0 && Height < 0)
            {
                return new Point(StartX - halfWidth, StartY - halfHeight);
            }

            if (Width < 0 && Height >= 0)
            {
                return new Point(StartX - halfWidth, StartY + halfHeight);
            }

            if (Width >= 0 && Height < 0)
            {
                return new Point(StartX + halfWidth, StartY - halfHeight);
            }

            return new Point(StartX + halfWidth, StartY + halfHeight);
        }
    }
}
