using ZWave.Models;

namespace ZWave.Services
{
    public interface IConfigurationService
    {
        Task GetConfig_DonorLifting();
        Task GetConfig_ProcessTable();
        Task GetConfig_InspectionVision();
        Task GetConfig_MotionAxisControl();

        IEnumerable<AxisControlModel> AxisControlModels { get; }
        IEnumerable<DonorLiftingModuleConfigurationModel> DonorLiftingModuleConfigurationModels { get; }
        IEnumerable<ProcessTableConfigurationModel> ProcessTableConfigurationModels { get; }
        IEnumerable<InspectionVisionConfigurationModel> InspectionVisionConfigurationModels { get; }
        IEnumerable<MotionAxisControlConfigurationModel> MotionAxisControlConfigurationModels { get; }
        Guid MachineId { get; }

        int CriticalActionTimeout { get; }
    }
}
