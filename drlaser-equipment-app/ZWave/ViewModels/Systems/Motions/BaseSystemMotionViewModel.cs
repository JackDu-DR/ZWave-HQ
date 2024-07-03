using ZWave.Models;
using ZWave.Models.Interface;

namespace ZWave.ViewModels.Systems.Motions
{
    public abstract class BaseSystemMotionViewModel : BaseViewModel
    {
        private readonly ISystemMotionDataModel _systemMotionData;
        public ISystemMotionDataModel SystemMotionData => _systemMotionData;

        public BaseSystemMotionViewModel()
        {
            _systemMotionData = SystemMotionDataModel.GetInstance();
            _systemMotionData.AxisList = ConfigurationService.AxisControlModels;
        }
    }
}
