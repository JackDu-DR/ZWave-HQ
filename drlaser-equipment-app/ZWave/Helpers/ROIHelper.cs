using SkiaSharp;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.Helpers
{
    public static class ROIHelper
    {
        public static byte[] GetROIImageBytes(byte[] imageData, IShapeROI teachingROI)
        {
            SKBitmap imageBitmap = CreateBitmapFromBytes(imageData);

            var bitmap = new SKBitmap(imageBitmap.Width, imageBitmap.Height);

            using (var surface = SKSurface.Create(new SKImageInfo(imageBitmap.Width, imageBitmap.Height), bitmap.GetPixels()))
            {
                var canvas = surface.Canvas;

                canvas.Clear(SKColors.White);
                if (imageBitmap != null)
                {
                    canvas.DrawBitmap(imageBitmap, new SKRect(0, 0, imageBitmap.Width, imageBitmap.Height));
                }

                var ratio = (float)(imageBitmap.Width / DimensionHelper.MotionVideoWidth);
                CanvasHelper.DrawROITeachingShape(canvas, teachingROI, SKColors.Green, true, SKColors.White, ratio: ratio);

                // Enable these code for saving to test
                //var filePath = Path.Combine("D:\\", "myImage.png");
                //using (var stream = File.Create(filePath))
                //{
                //    bitmap.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
                //}

                using (SKImage image = SKImage.FromBitmap(bitmap))
                using (SKData data = image.Encode())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        data.SaveTo(stream);
                        return stream.ToArray();
                    }
                }
            }
        }

        private static SKBitmap CreateBitmapFromBytes(byte[] imageData)
        {
            using (SKManagedStream stream = new SKManagedStream(new MemoryStream(imageData)))
            {
                return SKBitmap.Decode(stream);
            }
        }
    }
}
