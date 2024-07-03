using CommonLib.DTO.Implementation;
using CommonLib.DTO.Implementation.Configuration;
using System.Collections;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Interface
{
    [Guid("9D02E477-90DF-48BE-84FC-12E3413793E7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IConfigurationDTO : IBaseDTO
    {
        string MachineId { get; set; } 
        
        /// <summary>
        /// Timeout in miliseconds
        /// </summary>
        int CriticalActionTimeoutDefault { get; set; }

        void AddAxisConfig(AxisConfigDTO axisConfigDTO);
        void AddProcessTableConfig(ProcessTableConfigurationDTO processTableConfigurationDTO);
        void AddInspectionVisionConfig(InspectionVisionConfigurationDTO inspectionVisionConfigurationDTO);
        void AddMotionAxisControlConfig(MotionAxisControlConfigurationDTO motionAxisControlConfigurationDTO);
        void AddDonorLiftingModuleConfig(DonorLiftingModuleConfigurationDTO donorLiftingModuleConfigurationDTO);

        //For C++ usage, because the COM does not expose base method from child in C++.
        new void LoadDataFromJson(string json);
    }
}
