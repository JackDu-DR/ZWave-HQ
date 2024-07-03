using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("F4941F23-4130-4A01-A9BD-C485FBBDBEBA")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IRectangleROIDTO : IBaseDTO
    {
        string ShapeName { get; set; }
        double RowCenter { get; set; }
        double ColumnCenter { get; set; }
        double Row1 { get; set; }
        double Column1 { get; set; }
        double Row2 { get; set; }
        double Column2 { get; set; }
        double Phi { get; set; }
        byte[] ImageBytes { get; set; }
        double Score { get; set; }

        new void LoadDataFromJson(string json);
    }
}
