using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;

namespace ZWave.Models
{
    public class ProcessTableModel : BaseModel
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

        private double _txAxisPosition;
        public double TXAxisPosition
        {
            get => _txAxisPosition;
            set => SetProperty(ref _txAxisPosition, value);
        }

        private double _tyAxisPosition;
        public double TYAxisPosition
        {
            get => _tyAxisPosition;
            set => SetProperty(ref _tyAxisPosition, value);
        }

        private double _zAxisPosition;
        public double ZAxisPosition
        {
            get => _zAxisPosition;
            set => SetProperty(ref _zAxisPosition, value);
        }

        private bool _isTempSubstrateChuckVacuumEnabled;
        public bool IsTempSubstrateChuckVacuumEnabled
        {
            get => _isTempSubstrateChuckVacuumEnabled;
            set
            {
                if (SetProperty(ref _isTempSubstrateChuckVacuumEnabled, value))
                    SetIsOutputEnabled(value);
            }
        }

        public void SetIsOutputEnabled(bool isOutputEnabled)
        {
            if (_isSubstrateChuckVacuumEnabled != isOutputEnabled)
            {
                _isSubstrateChuckVacuumEnabled = isOutputEnabled;
                OnPropertyChanged(nameof(IsSubstrateChuckVacuumEnabled));
            }
        }

        private bool _isSubstrateChuckVacuumEnabled;
        public bool IsSubstrateChuckVacuumEnabled
        {
            get => _isSubstrateChuckVacuumEnabled;
            set
            {
                if (SetProperty(ref _isSubstrateChuckVacuumEnabled, value))
                    OnRequestUpdate();
            }
        }

        protected virtual void OnRequestUpdate()
        {
            RequestUpdate?.Invoke(this, EventArgs.Empty);
        }

        private ProTableUIElement _proTableUIElement;
        public ProTableUIElement ProTableUIElement
        {
            get => _proTableUIElement;
            set => SetProperty(ref _proTableUIElement, value);
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

        public IEnumerable<ProcessTableConfigurationModel> ProcessTableConfigurationModelData { get; set; }

        public ProcessTableModel()
        {
            ProcessTableConfigurationModelData = new List<ProcessTableConfigurationModel>();
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var processTableDTO = JsonConvert.DeserializeObject<ProcessTableDTO>(baseDTO.ToString());

            XAxisPosition = processTableDTO.XAxisPosition;
            YAxisPosition = processTableDTO.YAxisPosition;
            TXAxisPosition = processTableDTO.TXAxisPosition;
            TYAxisPosition = processTableDTO.TYAxisPosition;
            ZAxisPosition = processTableDTO.ZAxisPosition;
            IsTempSubstrateChuckVacuumEnabled = processTableDTO.IsSubstrateChuckVacuumEnabled;

        }
    }
}
