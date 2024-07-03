using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;

namespace ZWave.Models
{
    public class DonorLiftingModuleModel : BaseModel
    {
        public event EventHandler RequestUpdate;

        private double _xAxisPosition;
        public double XAxisPosition
        {
            get => _xAxisPosition;
            set => SetProperty(ref _xAxisPosition, value);
        }

        private double _yAxisPosition;
        public double YAxisPosition
        {
            get => _yAxisPosition;
            set => SetProperty(ref _yAxisPosition, value);
        }

        private double _zAxisPosition;
        public double ZAxisPosition
        {
            get => _zAxisPosition;
            set => SetProperty(ref _zAxisPosition, value);
        }

        private bool _isTempDonorChuckVacuumEnabled;
        public bool IsTempDonorChuckVacuumEnabled
        {
            get => _isTempDonorChuckVacuumEnabled;
            set
            {
                if (SetProperty(ref _isTempDonorChuckVacuumEnabled, value))
                    SetIsDonorChuckVacuumOutputEnabled(value);
            }
        }

        private bool _isDonorChuckVacuumEnabled;
        public bool IsDonorChuckVacuumEnabled
        {
            get => _isDonorChuckVacuumEnabled;
            set
            {
                if (SetProperty(ref _isDonorChuckVacuumEnabled, value))
                    OnRequestUpdate();
            }
        }

        public void SetIsDonorChuckVacuumOutputEnabled(bool isOutputEnabled)
        {
            if (_isDonorChuckVacuumEnabled != isOutputEnabled)
            {
                _isDonorChuckVacuumEnabled = isOutputEnabled;
                OnPropertyChanged(nameof(IsDonorChuckVacuumEnabled));
            }
        }

        protected virtual void OnRequestUpdate()
        {
            RequestUpdate?.Invoke(this, EventArgs.Empty);
        }

        private DonorLifterUIElement _donorLifterUIElement;
        public DonorLifterUIElement DonorLifterUIElement
        {
            get => _donorLifterUIElement;
            set => SetProperty(ref _donorLifterUIElement, value);
        }

        private MoveDirection _moveDirection;
        public MoveDirection MoveDirection
        {
            get => _moveDirection;
            set => SetProperty(ref _moveDirection, value);
        }

        private MotionCmd _motionCmd;
        public MotionCmd MotionCmd
        {
            get => _motionCmd;
            set => SetProperty(ref _motionCmd, value);
        }

        private CameraSelect _cameraSelect;
        public CameraSelect CameraSelect
        {
            get => _cameraSelect;
            set => SetProperty(ref _cameraSelect, value);
        }

        private double _moveRel;
        public double MoveRel
        {
            get => _moveRel;
            set => SetProperty(ref _moveRel, value);
        }

        public IEnumerable<DonorLiftingModuleConfigurationModel> DonorLiftingModuleConfigurationData { get; set; }

        private DonorLiftingModuleConfigurationModel _selectedAxis;
        public DonorLiftingModuleConfigurationModel SelectedAxis
        {
            get => _selectedAxis;
            set => SetProperty(ref _selectedAxis, value);
        }

        public DonorLiftingModuleModel()
        {
            DonorLiftingModuleConfigurationData = new List<DonorLiftingModuleConfigurationModel>();
            SelectedAxis = new DonorLiftingModuleConfigurationModel();
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var donorLiftingModuleDTO = JsonConvert.DeserializeObject<DonorLiftingModuleDTO>(baseDTO.ToString());

            XAxisPosition = donorLiftingModuleDTO.XAxisPosition;
            YAxisPosition = donorLiftingModuleDTO.YAxisPosition;
            ZAxisPosition = donorLiftingModuleDTO.ZAxisPosition;
            IsTempDonorChuckVacuumEnabled = donorLiftingModuleDTO.IsDonorChuckVacuumEnabled;
        }
    }
}
