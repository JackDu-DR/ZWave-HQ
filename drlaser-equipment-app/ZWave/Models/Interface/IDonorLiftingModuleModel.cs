namespace ZWave.Models.Interface
{
    public interface IDonorLiftingModuleModel
    {
        IEnumerable<DonorLiftingModuleConfigurationModel> DonorLiftingModuleConfigurationData { get; set; }

    }
}
