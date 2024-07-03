using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("4D9E54BC-4D63-47E0-AD1E-56A86B3C4162")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class EllipseROIDTO : BaseDTO, IEllipseROIDTO
    {
        public string ShapeName { get; set; }
        public double RowCenter { get; set; }
        public double ColumnCenter { get; set; }
        public double Row1 { get; set; }
        public double Column1 { get; set; }
        public double Radius1 { get; set; }
        public double Radius2 { get; set; }
        public double Phi { get; set; }
        public byte[] ImageBytes { get; set; }
        public double Score { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<EllipseROIDTO>(json);

            if (newObject == null) return;
            ShapeName = newObject.ShapeName;
            RowCenter = newObject.RowCenter;
            ColumnCenter = newObject.ColumnCenter;
            Row1 = newObject.Row1;
            Column1 = newObject.Column1;
            Radius1 = newObject.Radius1;
            Radius2 = newObject.Radius2;
            Phi = newObject.Phi;
            ImageBytes = newObject.ImageBytes;
            Score = newObject.Score;
        }
    }
}
