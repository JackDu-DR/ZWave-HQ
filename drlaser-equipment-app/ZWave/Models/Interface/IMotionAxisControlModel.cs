namespace ZWave.Models.Interface
{
    internal class IMotionAxisControlModel
    {
        IEnumerable<MotionAxisControlConfigurationModel> MotionAxisControlConfigurationModelData { get; set; }
        MotionAxisControlConfigurationModel SelectedAxis { get; set; }
    }
}
