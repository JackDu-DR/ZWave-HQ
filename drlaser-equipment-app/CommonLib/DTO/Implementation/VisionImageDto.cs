using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("3BA95CE6-C90C-438A-974B-9B0661300064")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class VisionImageDto : BaseDTO, IVisionImageDto
    {
        public double FullImageWidth { get; set; }
        public double FullImageHeight { get; set; }
        public double ImageVisualWidth { get; set; }
        public double ImageVisualHeight { get; set; }
        public double ScrollX { get; set; }
        public double ScrollY { get; set; }
        public int Ratio { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<VisionImageDto>(json);

            if (newObject != null)
            {
                FullImageWidth = newObject.FullImageWidth;
                FullImageHeight = newObject.FullImageHeight;
                ImageVisualWidth = newObject.ImageVisualWidth;
                ImageVisualHeight = newObject.ImageVisualHeight;
                ScrollX = newObject.ScrollX;
                ScrollY = newObject.ScrollY;
                Ratio = newObject.Ratio;
            }
        }
    }
}
