using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTO.Interface
{
    [Guid("93049125-4644-41E5-8BD8-ACAA803BE082")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IImageStreamDTO : IBaseDTO
    {
    	double ScrollX { get; set; }
        double ScrollY { get; set; }
        int Ratio { get; set; }
        double FullImageWidth { get; set; }
        double FullImageHeight { get; set; }
        double VisualImageWidth { get; set; }
        double VisualImageHeight { get; set; }
        long Timestamp { get; set; }
        byte[] ImageBytes { get; set; }
        
        byte[] StreamData { get; set; }

        //public string timeStamp { get; set; }

        public string ImageID { get; set; }

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);

        byte[] ConvertToBytes(object obj);
    }
}
