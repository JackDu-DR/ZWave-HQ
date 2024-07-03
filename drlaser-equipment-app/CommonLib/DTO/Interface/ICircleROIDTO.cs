using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("9F1ABD47-0164-4971-ACDC-449659CF29B8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface ICircleROIDTO : IBaseDTO
    {
        string ShapeName { get; set; }
        double RowCenter { get; set; }
        double ColumnCenter { get; set; }
        double Radius { get; set; }

        byte[] ImageBytes { get; set; }
        double Score { get; set; }

        new void LoadDataFromJson(string json);
    }
}
