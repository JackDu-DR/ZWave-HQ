using CommonLib.DTO.Implementation.Configuration;
using CommonLib.DTO.Interface;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("0E3431A7-AFB0-4212-AA46-272A6B85AB13")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ConfigurationDTO : IConfigurationDTO
    {
        [ComVisible(false)]
        public List<AxisConfigDTO> AxisConfigsDTOs { get; set; } = new List<AxisConfigDTO>();

        [ComVisible(false)]
        public List<MotionAxisControlConfigurationDTO> MotionAxisControlConfigurationDTOs { get; set; } = new List<MotionAxisControlConfigurationDTO>();

        [ComVisible(false)]
        public List<ProcessTableConfigurationDTO> ProcessTableConfigurationDTOs { get; set; } = new List<ProcessTableConfigurationDTO>();

        [ComVisible(false)]
        public List<InspectionVisionConfigurationDTO> InspectionVisionConfigurationDTOs { get; set; } = new List<InspectionVisionConfigurationDTO>();

        [ComVisible(false)]
        public List<DonorLiftingModuleConfigurationDTO> DonorLiftingModuleConfigurationDTOs { get; set; } = new List<DonorLiftingModuleConfigurationDTO>();

        public string MachineId { get ; set ; }
        public int CriticalActionTimeoutDefault { get; set; }

        public void AddAxisConfig(AxisConfigDTO axisConfigDTO)
        {
            if (AxisConfigsDTOs == null)
            {
                AxisConfigsDTOs = new List<AxisConfigDTO>();
            }

            AxisConfigsDTOs.Add(axisConfigDTO);
        }

        public void AddMotionAxisControlConfig(MotionAxisControlConfigurationDTO motionAxisControlConfigurationDTO)
        {
            if (MotionAxisControlConfigurationDTOs == null)
            {
                MotionAxisControlConfigurationDTOs = new List<MotionAxisControlConfigurationDTO>();
            }

            MotionAxisControlConfigurationDTOs.Add(motionAxisControlConfigurationDTO);
        }
        public void AddProcessTableConfig(ProcessTableConfigurationDTO processTableConfigurationDTO)
        {
            if (ProcessTableConfigurationDTOs == null)
            {
                ProcessTableConfigurationDTOs = new List<ProcessTableConfigurationDTO>();
            }

            ProcessTableConfigurationDTOs.Add(processTableConfigurationDTO);
        }
        public void AddInspectionVisionConfig(InspectionVisionConfigurationDTO inspectionVisionConfigurationDTO)
        {
            if (InspectionVisionConfigurationDTOs == null)
            {
                InspectionVisionConfigurationDTOs = new List<InspectionVisionConfigurationDTO>();
            }

            InspectionVisionConfigurationDTOs.Add(inspectionVisionConfigurationDTO);
        }

        public void AddDonorLiftingModuleConfig(DonorLiftingModuleConfigurationDTO donorLiftingModuleConfigurationDTO)
        {
            if (DonorLiftingModuleConfigurationDTOs == null)
            {
                DonorLiftingModuleConfigurationDTOs = new List<DonorLiftingModuleConfigurationDTO>();
            }

            DonorLiftingModuleConfigurationDTOs.Add(donorLiftingModuleConfigurationDTO);
        }

        public void LoadDataFromJson(string json)
        {
        }
    }
}
