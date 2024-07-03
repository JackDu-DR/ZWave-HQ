using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;
using ZWave.Enums;

namespace ZWave.Models
{
    public class MotionAxisControlModel : BaseModel
    {
        private double _actualMovePos;
        public double ActualMovePos
        {
            get => _actualMovePos;
            set => SetProperty(ref _actualMovePos, value);
        }

        private double _actualVelPos;
        public double ActualVelPos
        {
            get => _actualVelPos;
            set => SetProperty(ref _actualVelPos, value);
        }

        private double _actualAcclPos;
        public double ActualAcclPos
        {
            get => _actualAcclPos;
            set => SetProperty(ref _actualAcclPos, value);
        }


        private double _actualJerkPos;
        public double ActualJerkPos
        {
            get => _actualJerkPos;
            set => SetProperty(ref _actualJerkPos, value);
        }

        private double _movePosRel;
        public double MovePosRel
        {
            get => _movePosRel;
            set => SetProperty(ref _movePosRel, value);
        }
        private double _movePosAbs;
        public double MovePosAbs
        {
            get => _movePosAbs;
            set => SetProperty(ref _movePosAbs, value);
        }

        private double _velPos;
        public double VelPos
        {
            get => _velPos;
            set => SetProperty(ref _velPos, value);
        }

        private double _acclPos;
        public double AcclPos
        {
            get => _acclPos;
            set => SetProperty(ref _acclPos, value);
        }


        private double _jerkPos;
        public double JerkPos
        {
            get => _jerkPos;
            set => SetProperty(ref _jerkPos, value);
        }

        private AxisSelection _axisSelection;
        public AxisSelection AxisSelection
        {
            get => _axisSelection;
            set => SetProperty(ref _axisSelection, value);
        }

        private MotionUIElement _motionUIElement;
        public MotionUIElement MotionUIElement
        {
            get => _motionUIElement;
            set => SetProperty(ref _motionUIElement, value);
        }

        private MotionCmd _motionCmd;
        public MotionCmd MotionCmd
        {
            get => _motionCmd;
            set => SetProperty(ref _motionCmd, value);
        }

        public string Unit { get; set; } = "mm";

        public string PositionUnit
        {
            get => Unit;
        }
        public string VelocityUnit => Unit;
        public string AccelUnit => Unit + "/s" + '\u00B2';
        public string JerkUnit => Unit + "/s" + '\u00B3';

        public IEnumerable<MotionAxisControlConfigurationModel> MotionAxisControlConfigurationModelData { get; set; }

        private MotionAxisControlConfigurationModel _selectedAxis;
        public MotionAxisControlConfigurationModel SelectedAxis
        {
            get => _selectedAxis;
            set => SetProperty(ref _selectedAxis, value);
        }

        public MotionAxisControlModel()
        {
            MotionAxisControlConfigurationModelData = new List<MotionAxisControlConfigurationModel>();
            SelectedAxis = new MotionAxisControlConfigurationModel();
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(baseDTO.ToString());

            MovePosRel = motionAxisControlDTO.MovePos;
            MovePosAbs = motionAxisControlDTO.MovePos;
            VelPos = motionAxisControlDTO.VelPos;
            AcclPos = motionAxisControlDTO.AcclPos;
            JerkPos = motionAxisControlDTO.JerkPos;

            ActualMovePos = motionAxisControlDTO.ActualMovePos;
            ActualVelPos = motionAxisControlDTO.ActualVelPos;
            ActualAcclPos = motionAxisControlDTO.ActualAcclPos;
            ActualJerkPos = motionAxisControlDTO.ActualJerkPos;
        }
    }
}
