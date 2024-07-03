using CommonLib.ClassHierarchy.Interface;
using CommonLib.DTO.Implementation;
using CommonLib.DTO.Implementation.Configuration;
using CommonLib.MessageHandler.Interface;
using Newtonsoft.Json;
using System.Diagnostics;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IAppMessageHandler _appMessageHandler;
        private readonly IMaster _master;

        private string ConfigurationId => _master.Configuration.Self;
        private string MotionAxisControlConfigurationId => _master.Configuration.MotionAxisControl; 
        private string DonorLiftingModuleConfigurationId => _master.Configuration.DonorLiftingModule;
        private string ProcessTableConfigurationId => _master.Configuration.ProcessTable;
        private string InspectionVisionConfigurationId => _master.Configuration.InspectionVision;

        public IEnumerable<AxisControlModel> AxisControlModels { get; private set; }
        public IEnumerable<DonorLiftingModuleConfigurationModel> DonorLiftingModuleConfigurationModels { get; private set; }
        public IEnumerable<ProcessTableConfigurationModel> ProcessTableConfigurationModels { get; private set; }
        public IEnumerable<InspectionVisionConfigurationModel> InspectionVisionConfigurationModels { get; private set; }
        public IEnumerable<MotionAxisControlConfigurationModel> MotionAxisControlConfigurationModels { get; private set; }

        public Guid MachineId { get; private set; }
        public int CriticalActionTimeout { get; private set; }

        public ConfigurationService(IAppMessageHandler appMessageHandler, IMaster master)
        {
            _appMessageHandler = appMessageHandler;
            _master = master;

            AxisControlModels = new List<AxisControlModel>();
            DonorLiftingModuleConfigurationModels = new List<DonorLiftingModuleConfigurationModel>();
            ProcessTableConfigurationModels = new List<ProcessTableConfigurationModel>();
            InspectionVisionConfigurationModels = new List<InspectionVisionConfigurationModel>();
            MotionAxisControlConfigurationModels = new List<MotionAxisControlConfigurationModel>();
        }

        public async Task GetConfig_DonorLifting()
        {
            var response = await _appMessageHandler.Request(DonorLiftingModuleConfigurationId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);
            LoadDataFromDTOJson_DonorLifting(message.Data);
        }
        public async Task GetConfig_ProcessTable()
        {
            var response = await _appMessageHandler.Request(ProcessTableConfigurationId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);
            LoadDataFromDTOJson_ProcessTable(message.Data);
        }
        public async Task GetConfig_InspectionVision()
        {
            var response = await _appMessageHandler.Request(InspectionVisionConfigurationId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);
            LoadDataFromDTOJson_InspectionVision(message.Data);
        }
        public async Task GetConfig_MotionAxisControl()
        {
            var response = await _appMessageHandler.Request(MotionAxisControlConfigurationId, null);
            var strResponse = StringHelper.ByteArrayToString(response);
            var message = JsonConvert.DeserializeObject<Message>(strResponse);
            LoadDataFromDTOJson_MotionAxisControl(message.Data);
        }

        private void LoadDataFromDTOJson_DonorLifting(object baseDTO)
        {
            var configurationDTO = JsonConvert.DeserializeObject<ConfigurationDTO>(baseDTO.ToString());
            DonorLiftingModuleConfigurationModels = GetDonorLiftingModuleConfigurationModelsFromDTOs(configurationDTO.DonorLiftingModuleConfigurationDTOs);
            MachineId = new Guid(configurationDTO.MachineId);
            CriticalActionTimeout = configurationDTO.CriticalActionTimeoutDefault;
        }

        private void LoadDataFromDTOJson_ProcessTable(object baseDTO)
        {
            var configurationDTO = JsonConvert.DeserializeObject<ConfigurationDTO>(baseDTO.ToString());
            ProcessTableConfigurationModels = GetProcessTableConfigurationModelsFromDTOs(configurationDTO.ProcessTableConfigurationDTOs);
            MachineId = new Guid(configurationDTO.MachineId);
            CriticalActionTimeout = configurationDTO.CriticalActionTimeoutDefault;
        }

        private void LoadDataFromDTOJson_InspectionVision(object baseDTO)
        {
            var configurationDTO = JsonConvert.DeserializeObject<ConfigurationDTO>(baseDTO.ToString());
            InspectionVisionConfigurationModels = GetInspectionVisionConfigurationModelsFromDTOs(configurationDTO.InspectionVisionConfigurationDTOs);
            MachineId = new Guid(configurationDTO.MachineId);
            CriticalActionTimeout = configurationDTO.CriticalActionTimeoutDefault;
        }

        private void LoadDataFromDTOJson_MotionAxisControl(object baseDTO)
        {
            var configurationDTO = JsonConvert.DeserializeObject<ConfigurationDTO>(baseDTO.ToString());
            MotionAxisControlConfigurationModels = GetMotionAxisControlConfigurationModelsFromDTOs(configurationDTO.MotionAxisControlConfigurationDTOs);
            MachineId = new Guid(configurationDTO.MachineId);
            CriticalActionTimeout = configurationDTO.CriticalActionTimeoutDefault;
        }

        private IEnumerable<AxisControlModel> GetAxisControlModelsFromDTOs(IEnumerable<AxisConfigDTO> axisConfigDTOs)
        {
            foreach (var axisConfigDTO in axisConfigDTOs)
            {
                yield return AxisControlModel.GetAxisControlModelFromDTO(axisConfigDTO);
            }
        }

        private IEnumerable<DonorLiftingModuleConfigurationModel> GetDonorLiftingModuleConfigurationModelsFromDTOs(IEnumerable<DonorLiftingModuleConfigurationDTO> donorLiftingModuleConfigurationDTOs)
        {
            foreach (var donorLiftingModuleConfigurationDTO in donorLiftingModuleConfigurationDTOs)
            {
                yield return DonorLiftingModuleConfigurationModel.GetDonorLiftingModuleConfigurationModelFromDTO(donorLiftingModuleConfigurationDTO);
            }
        }

        private IEnumerable<ProcessTableConfigurationModel> GetProcessTableConfigurationModelsFromDTOs(IEnumerable<ProcessTableConfigurationDTO> processTableConfigurationDTOs)
        {
            foreach (var processTableConfigurationDTO in processTableConfigurationDTOs)
            {
                yield return ProcessTableConfigurationModel.GetProcessTableConfigurationModelFromDTO(processTableConfigurationDTO);
            }
        }

        private IEnumerable<InspectionVisionConfigurationModel> GetInspectionVisionConfigurationModelsFromDTOs(IEnumerable<InspectionVisionConfigurationDTO> inspectionVisionConfigurationDTOs)
        {
            foreach (var inspectionVisionConfigurationDTO in inspectionVisionConfigurationDTOs)
            {
                yield return InspectionVisionConfigurationModel.GetInspectionVisionConfigurationModelFromDTO(inspectionVisionConfigurationDTO);
            }
        }

        private IEnumerable<MotionAxisControlConfigurationModel> GetMotionAxisControlConfigurationModelsFromDTOs(IEnumerable<MotionAxisControlConfigurationDTO> motionAxisControlConfigurationDTOs)
        {
            foreach (var motionAxisControlConfigurationDTO in motionAxisControlConfigurationDTOs)
            {
                yield return MotionAxisControlConfigurationModel.GetMotionAxisControlConfigurationModelFromDTO(motionAxisControlConfigurationDTO);
            }
        }
    }
}
