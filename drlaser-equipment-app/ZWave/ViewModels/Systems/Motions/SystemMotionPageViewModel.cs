using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using ZWave.Models;

namespace ZWave.ViewModels.Systems.Motions
{
    public partial class SystemMotionPageViewModel : BaseSystemMotionViewModel
    {
        //[ObservableProperty]
        //private IList<AxisControlModel> _axisList;

        //[ObservableProperty]
        //private AxisControlModel _selectedAxis;

        //private string ChangeAxisId => Master.System.Motion.MotionAxisControl.ChangeAxis;

        //[ObservableProperty]
        //SystemMotionDataModel _systemMotionDataModel;

        //[ObservableProperty]
        //List<string> _presetAxisSource;

        public SystemMotionPageViewModel()
        {
            //_presetAxisSource = new List<string>() { "ProTableX", "ProsTableY", "ProsUpLookCamZ", "ProsDownLookCamZ", "CaswayDonorLifterX", "CaswayDonorLifterY", "CaswayDonorLifterZ", "CaswaySubLifterZ", "BuffTableX", "BuffTableY", "BuffTableT", "BuffUpLookCamZ", "BuffThinkFocusZ" };
            //SystemMotionDataModel = new SystemMotionDataModel();

            //_axisList = SystemMotionData.AxisList.ToList();
            //PropertyChanged += OnPropertyChanged;
        }

        //[RelayCommand]
        //void SelectedAxisChanged()
        //{
        //    var motionAxisControlDTO = new MotionAxisControlDTO()
        //    {
        //        AxisSelection = (AxisSelection)Enum.Parse(typeof(AxisSelection), SystemMotionDataModel.SelectedAxisType),
        //    };

        //    AppMessageHandler.PublishToMachine(ChangeAxisId, motionAxisControlDTO);
        //}

        //private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(SelectedAxis))
        //    {
        //        SystemMotionData.SelectedAxis = SelectedAxis;
        //    }
        //}
    }
}