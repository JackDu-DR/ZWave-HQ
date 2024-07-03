namespace ZWave.Models.Interface
{
    public interface IInspectionVisionModel
    {
        IEnumerable<InspectionVisionConfigurationModel> InspectionVisionConfigurationModelData { get; set; }
        InspectionVisionConfigurationModel SelectedCamera { get; set; }
    }
}
