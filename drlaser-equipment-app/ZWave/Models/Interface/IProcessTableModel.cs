namespace ZWave.Models.Interface
{
    public interface IProcessTableModel
    {
        IEnumerable<ProcessTableConfigurationModel> ProcessTableConfigurationModelData { get; set; }
    }
}
