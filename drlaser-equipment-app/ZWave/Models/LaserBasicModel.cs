using CommonLib.DTO.Implementation;
using Newtonsoft.Json;

namespace ZWave.Models
{
    public class LaserBasicModel : BaseModel
    {
        //The OutputId is declared twice in LaserBasicModel and LaserBasicPageViewModel,
        //it should be gathered in one place to avoid the duplication.
        //https://sioux.atlassian.net/browse/DRL-213
        private string OutputId => Master.System.Laser.LaserBasic.Output;

        private bool _isOutputEnabled;
        public bool IsOutputEnabled
        {
            get => _isOutputEnabled;
            set
            {
                if (SetProperty(ref _isOutputEnabled, value))
                {
                    AppMessageHandler.PublishToMachine(OutputId, value);
                }
            }
        }

        public void SetIsOutputEnabled(bool isOutputEnabled)
        {
            _isOutputEnabled = isOutputEnabled;
            OnPropertyChanged(nameof(IsOutputEnabled));
        }

        private string _actualPresetControl;
        public string ActualPresetControl
        {
            get => _actualPresetControl;
            set => SetProperty(ref _actualPresetControl, value);
        }

        private string _presetControl;
        public string PresetControl
        {
            get => _presetControl;
            set => SetProperty(ref _presetControl, value);
        }

        private double _actualAttenuatorPercentage;
        public double ActualAttenuatorPercentage
        {
            get => _actualAttenuatorPercentage;
            set => SetProperty(ref _actualAttenuatorPercentage, value);
        }

        private double _attenuatorPercentage;
        public double AttenuatorPercentage
        {
            get => _attenuatorPercentage;
            set => SetProperty(ref _attenuatorPercentage, value);
        }

        private double _actualPPDivider;
        public double ActualPPDivider
        {
            get => _actualPPDivider;
            set => SetProperty(ref _actualPPDivider, value);
        }

        private double _pPDivider;
        public double PPDivider
        {
            get => _pPDivider;
            set => SetProperty(ref _pPDivider, value);
        }

        private double _actualPulseDuration;
        public double ActualPulseDuration
        {
            get => _actualPulseDuration;
            set => SetProperty(ref _actualPulseDuration, value);
        }

        private double _pulseDuration;
        public double PulseDuration
        {
            get => _pulseDuration;
            set => SetProperty(ref _pulseDuration, value);
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var laserBasicDTO = JsonConvert.DeserializeObject<LaserBasicDTO>(baseDTO.ToString());

            SetIsOutputEnabled(laserBasicDTO.IsOutputEnabled);
            ActualAttenuatorPercentage = AttenuatorPercentage = laserBasicDTO.AttenuatorPercentage;
            ActualPPDivider = PPDivider = laserBasicDTO.PPDivider;
            ActualPulseDuration = PulseDuration = laserBasicDTO.PulseDuration;
            ActualPresetControl = PresetControl = ((int)laserBasicDTO.PresetControl).ToString();
        }
    }
}