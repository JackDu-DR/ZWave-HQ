using CommunityToolkit.Mvvm.ComponentModel;

namespace ZWave.ViewModels
{
    public partial class SystemPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private bool _isShowingModule1 = false;

        [ObservableProperty]
        private bool _isShowingCausewaySystem = false;

        [ObservableProperty]
        private bool _isShowingProcessSystem = false;

        [ObservableProperty]
        private bool _isShowingLaserEngine = false;

    }
}
