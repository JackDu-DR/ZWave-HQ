using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("D678E411-F542-431C-8F44-92CB85162212")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class RectangleROIDTO : BaseDTO, IRectangleROIDTO
    {
        public string ShapeName { get; set; }
        public double RowCenter { get; set; }
        public double ColumnCenter { get; set; }
        public double Row1 { get; set; }
        public double Column1 { get; set; }
        public double Row2 { get; set; }
        public double Column2 { get; set; }
        public double Phi { get; set; }
        public byte[] ImageBytes { get; set; }
        public double Score { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<RectangleROIDTO>(json);

            if (newObject == null) return;
            ShapeName = newObject.ShapeName;
            RowCenter = newObject.RowCenter;
            ColumnCenter = newObject.ColumnCenter;
            Row1 = newObject.Row1;
            Column1 = newObject.Column1;
            Row2 = newObject.Row2;
            Column2 = newObject.Column2;
            Phi = newObject.Phi;
            ImageBytes = newObject.ImageBytes;
            Score = newObject.Score;
        }
    }
}
