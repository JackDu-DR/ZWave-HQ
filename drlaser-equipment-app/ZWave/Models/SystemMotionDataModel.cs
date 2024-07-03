using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using Newtonsoft.Json;
using ZWave.Models.Interface;

namespace ZWave.Models
{
    public class SystemMotionDataModel : BaseModel, ISystemMotionDataModel
    {
        private string _selectedAxisType;
        public string SelectedAxisType
        {
            get => _selectedAxisType;
            set => SetProperty(ref _selectedAxisType, value);
        }

        private static ISystemMotionDataModel _instance;

        public IEnumerable<AxisControlModel> AxisList { get; set; }

        private AxisControlModel _selectedAxis;
        public AxisControlModel SelectedAxis
        {
            get => _selectedAxis;
            set => SetProperty(ref _selectedAxis, value);
        }

        public SystemMotionDataModel()
        {
            AxisList = new List<AxisControlModel>();
            SelectedAxis = new AxisControlModel();
        }

        public static ISystemMotionDataModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SystemMotionDataModel();
            }
            return _instance;
        }

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(baseDTO.ToString());
            SelectedAxisType = ((int)motionAxisControlDTO.AxisSelection).ToString();
        }
    }
}
