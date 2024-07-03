namespace ZWave.Models.Interface
{
    public interface ISystemMotionDataModel
    {
        IEnumerable<AxisControlModel> AxisList { get; set; }

        AxisControlModel SelectedAxis { get; set; }
    }
}
