using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("9A17C537-5B96-4912-92D0-196EF5D04E83")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IEllipseROIDTO : IBaseDTO
    {
        string ShapeName { get; set; }
        double RowCenter { get; set; }
        double ColumnCenter { get; set; }
        double Row1 { get; set; }
        double Column1 { get; set; }
        double Radius1 { get; set; }
        double Radius2 { get; set; }
        double Phi { get; set; }
        byte[] ImageBytes { get; set; }
        double Score { get; set; }

        new void LoadDataFromJson(string json);
    }
}
