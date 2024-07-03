using CommonLib.ClassHierarchy.Implementation;
using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CommonLib.DTO.Implementation
{
    [Guid("ED9EEE39-219C-4600-A1E4-6CF6326B9FAD")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ImageStreamDTO : BaseDTO, IImageStreamDTO
    {
    	public double ScrollX { get; set; }
        public double ScrollY { get; set; }
        public int Ratio { get; set; }
        public double FullImageWidth { get; set; }
        public double FullImageHeight { get; set; }
        public double VisualImageWidth { get; set; }
        public double VisualImageHeight { get; set; }
        public long Timestamp { get; set; }
        public byte[] ImageBytes { get; set; }
        
        public byte[] StreamData { get; set; }

        public string ImageID { get; set; }

        //public string timeStamp { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<ImageStreamDTO>(json);

            if (newObject != null)
            {
            	ScrollX = newObject.ScrollX;
            	ScrollY = newObject.ScrollY;
            	Ratio = newObject.Ratio;
            	FullImageWidth = newObject.FullImageWidth;
            	FullImageHeight = newObject.FullImageHeight;
            	VisualImageWidth = newObject.VisualImageWidth;
            	VisualImageHeight = newObject.VisualImageHeight;
            	Timestamp = newObject.Timestamp;
            	ImageBytes = newObject.ImageBytes;
            	
                StreamData = newObject.StreamData;
                ImageID = newObject.ImageID;
                //timeStamp = newObject.timeStamp;
            }

        }

        public byte[] ConvertToBytes(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
