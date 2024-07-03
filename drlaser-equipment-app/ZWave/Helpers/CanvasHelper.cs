using SkiaSharp;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.Helpers
{
    public static class CanvasHelper
    {
        public static void DrawImageBytes(SKCanvas canvas, byte[] imageData, float videoWidth, float videoHeight)
        {
            if (imageData == null) { return; }

            SKBitmap imageBitmap = CreateBitmapFromBytes(imageData);

            if (imageBitmap != null)
            {
                float density = 1;
#if ANDROID
                density = (float)DeviceDisplay.Current.MainDisplayInfo.Density;
#endif
                canvas.Clear();
                canvas.DrawBitmap(imageBitmap, new SKRect(0, 0, videoWidth * density, videoHeight * density));
            }
        }

        public static void DrawROITeachingShape(SKCanvas canvas, IShapeROI ROI, SKColor shapeColor, bool centerPointVisible, SKColor textColor, float ratio = 1, string scoreFormatString = "{0}")
        {
            var paint = new SKPaint()
            {
                Color = shapeColor,
                StrokeWidth = 2 * ratio,
                IsStroke = true,
                IsAntialias = true
            };

            var textYOffset = 10;

            if (ROI is Circle circle)
            {
                canvas.DrawCircle(circle.CenterX * ratio, circle.CenterY * ratio, circle.Radius * ratio, paint);

                if (centerPointVisible)
                {
                    DrawCenterPoint(canvas, circle.CenterX * ratio, circle.CenterY * ratio, shapeColor);
                }

                if (ROI.Score.HasValue)
                {
                    DrawText(string.Format(scoreFormatString, ROI.Score.Value.ToString()), canvas, circle.CenterX * ratio, (circle.CenterY - circle.Radius) * ratio - textYOffset, textColor);
                }
            }
            else if (ROI is Rectangle rectangle)
            {
                float centerX = (rectangle.StartX + rectangle.Width / 2) * ratio;
                float centerY = (rectangle.StartY + rectangle.Height / 2) * ratio;

                canvas.Save();
                canvas.Translate(centerX, centerY);
                canvas.RotateDegrees(rectangle.Phi);
                canvas.Translate(-centerX, -centerY);

                canvas.DrawRect(rectangle.StartX * ratio, rectangle.StartY * ratio, rectangle.Width * ratio, rectangle.Height * ratio, paint);

                canvas.Restore();

                if (centerPointVisible)
                {
                    DrawCenterPoint(canvas, (rectangle.StartX + rectangle.Width / 2) * ratio, (rectangle.StartY + rectangle.Height / 2) * ratio, shapeColor, rectangle.Phi);
                }

                if (ROI.Score.HasValue)
                {
                    DrawText(string.Format(scoreFormatString, ROI.Score.Value.ToString()), canvas, centerX, (centerY - rectangle.Height / 2) * ratio - textYOffset, textColor);
                }

                DrawArrowForShape(canvas, rectangle, shapeColor);
            }
            else if (ROI is Ellipse ellipse)
            {
                canvas.Save();
                canvas.Translate(ellipse.CenterX, ellipse.CenterY);
                canvas.RotateDegrees(ellipse.Phi);
                canvas.Translate(-ellipse.CenterX, -ellipse.CenterY);

                canvas.DrawOval(ellipse.CenterX * ratio, ellipse.CenterY * ratio, ellipse.RadiusX * ratio, ellipse.RadiusY * ratio, paint);
                canvas.Restore();

                if (centerPointVisible)
                {
                    DrawCenterPoint(canvas, ellipse.CenterX * ratio, ellipse.CenterY * ratio, shapeColor, ellipse.Phi);
                }

                if (ROI.Score.HasValue)
                {
                    DrawText(string.Format(scoreFormatString, ROI.Score.Value.ToString()), canvas, ellipse.CenterX * ratio, (ellipse.CenterY - ellipse.RadiusY) * ratio - textYOffset, textColor);
                }

                DrawArrowForShape(canvas, ellipse, shapeColor);
            }
        }

        public static void DrawCenterPoint(SKCanvas canvas, float X, float Y, SKColor color, float phi = 0)
        {
            float LINE_SIZE = 4;

            var paint = new SKPaint()
            {
                Color = color,
                StrokeWidth = 1,
            };

            canvas.Save();
            canvas.Translate(X, Y);
            canvas.RotateDegrees(phi);
            canvas.Translate(-X, -Y);

            canvas.DrawLine((int)(X - LINE_SIZE), (int)Y, (int)(X + LINE_SIZE + 1), (int)Y, paint);
            canvas.DrawLine((int)X, (int)(Y - LINE_SIZE), (int)X, (int)(Y + LINE_SIZE + 1), paint);

            canvas.Restore();
        }

        public static void DrawText(string text, SKCanvas canvas, float X, float Y, SKColor color, float phi = 0)
        {
            var paint = new SKPaint()
            {
                Color = color,
                StrokeWidth = 1,
                TextAlign = SKTextAlign.Center,
                TextSize = 20
            };
            canvas.DrawText(text, X, Y, paint);
        }

        public static float GetComplementaryPhi(float phi)
        {
            return 360 - phi;
        }

        public static void DrawCenterAxis(SKCanvas canvas, float width, float height)
        {
            var strokeWidth = 2;
            var paint = new SKPaint()
            {
                Color = SKColors.Green,
                StrokeWidth = strokeWidth,
                IsStroke = true,
                IsAntialias = true
            };
            float centerX = width / 2;
            float centerY = height / 2;
            canvas.DrawLine(new SKPoint(centerX, 0), new SKPoint(centerX, height), paint);
            canvas.DrawLine(new SKPoint(0, centerY), new SKPoint(width, centerY), paint);
        }
        private static SKBitmap CreateBitmapFromBytes(byte[] imageData)
        {
            using (SKManagedStream stream = new SKManagedStream(new MemoryStream(imageData)))
            {
                return SKBitmap.Decode(stream);
            }
        }

        public static void DrawArrowForShape(SKCanvas canvas, IShapeROI shape, SKColor color, float arrowSpace = 5)
        {
            float arrowBodyWidth = 10;
            float arrowBodyHeight = 4;
            float arrowHeadLength = 8;
            float arrowHeadBaseWidth = 12; // Make this larger than arrowBodyHeight to ensure a wider base for the arrowhead

            SKPaint paint = new()
            {
                Color = color,
                StrokeWidth = 1,
                IsStroke = true,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };

            float centerX;
            float centerY;
            float arrowStartX;

            if (shape is Rectangle rectangle)
            {
                centerX = rectangle.StartX + rectangle.Width / 2;
                centerY = rectangle.StartY + rectangle.Height / 2 + arrowBodyHeight / 2;

                arrowStartX = centerX + (Math.Abs(rectangle.Width) / 2 + arrowSpace);
            }
            else if (shape is Ellipse ellipse)
            {
                centerX = ellipse.CenterX;
                centerY = ellipse.CenterY + arrowBodyHeight / 2;

                arrowStartX = centerX + ellipse.RadiusX + arrowSpace;
            }
            else
            {
                return;
            }

            float arrowStartY = centerY;

            // Translate the arrow start point to the origin
            float translatedStartX = arrowStartX - centerX;
            float translatedStartY = arrowStartY - centerY;

            // Apply the rotation
            float cosTheta = (float)Math.Cos(shape.Phi * Math.PI / 180);
            float sinTheta = (float)Math.Sin(shape.Phi * Math.PI / 180);

            float rotatedStartX = cosTheta * translatedStartX - sinTheta * translatedStartY;
            float rotatedStartY = sinTheta * translatedStartX + cosTheta * translatedStartY;

            // Translate the rotated point back
            rotatedStartX += centerX;
            rotatedStartY += centerY - arrowBodyHeight / 2;

            // Calculate the end of the arrow body based on rotation
            float arrowEndX = rotatedStartX + arrowBodyWidth * cosTheta;
            float arrowEndY = rotatedStartY + arrowBodyWidth * sinTheta;

            // Calculate the points for the arrow head
            float headBaseLeftX = arrowEndX - arrowHeadBaseWidth / 2 * sinTheta;
            float headBaseLeftY = arrowEndY + arrowHeadBaseWidth / 2 * cosTheta;
            float headBaseRightX = arrowEndX + arrowHeadBaseWidth / 2 * sinTheta;
            float headBaseRightY = arrowEndY - arrowHeadBaseWidth / 2 * cosTheta;
            float arrowTipX = arrowEndX + arrowHeadLength * cosTheta;
            float arrowTipY = arrowEndY + arrowHeadLength * sinTheta;

            // Draw the arrow body
            canvas.Save();
            canvas.Translate(rotatedStartX, rotatedStartY);
            canvas.RotateDegrees(shape.Phi);
            canvas.Translate(-rotatedStartX, -rotatedStartY);
            SKRect arrowBody = new SKRect(rotatedStartX, rotatedStartY - arrowBodyHeight / 2, rotatedStartX + arrowBodyWidth, rotatedStartY + arrowBodyHeight / 2);
            canvas.DrawRect(arrowBody, paint);
            canvas.Restore();

            // Draw the arrow head
            SKPath arrowHeadPath = new SKPath();
            arrowHeadPath.MoveTo(headBaseLeftX, headBaseLeftY);
            arrowHeadPath.LineTo(arrowTipX, arrowTipY);
            arrowHeadPath.LineTo(headBaseRightX, headBaseRightY);
            arrowHeadPath.Close();
            canvas.DrawPath(arrowHeadPath, paint);
        }
    }
}
