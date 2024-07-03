using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("512B2BCC-54EE-489B-8820-EBFC5471106F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CircleROIDTO : BaseDTO, ICircleROIDTO
    {
        public string ShapeName { get; set; }
        public double Radius { get; set; }
        public double RowCenter { get; set; }
        public double ColumnCenter { get; set; }

        public byte[] ImageBytes { get; set; }
        public double Score { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<CircleROIDTO>(json);

            if (newObject == null) return;
            ShapeName = newObject.ShapeName;
            Radius = newObject.Radius;
            RowCenter = newObject.RowCenter;
            ColumnCenter = newObject.ColumnCenter;
            ImageBytes = newObject.ImageBytes;
            Score = newObject.Score;
        }
    }
}
