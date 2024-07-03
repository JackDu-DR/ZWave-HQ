using CommonLib.Enums;
using ZWave.ViewModels.Systems.Motions;

namespace ZWave.Views.Systems.Motions;

public partial class MotionAxisControlPage : ContentView
{
    public MotionAxisControlPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        LoadDefaultDataAfterConfigurationDone();
    }

    private void LoadDefaultDataAfterConfigurationDone()
    {
        bool loadCompleted = ((MotionAxisControlPageViewModel)BindingContext).LoadCompleted;

        Task.Run(() =>
        {
            if (loadCompleted)
                LoadConfigurationData();
            else
            {
                while (!loadCompleted)
                {
                    loadCompleted = ((MotionAxisControlPageViewModel)BindingContext).LoadCompleted;
                    if (loadCompleted)
                    {
                        LoadConfigurationData();
                    }
                }
            }

        });
    }

    private void LoadConfigurationData()
    {
        AxisSelector.SelectedItem = ((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlConfigurationModel.Count == 0 ? "" : ((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlConfigurationModel[0];
        if (((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlConfigurationModel.Count != 0)
        {
            ((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlModel.AxisSelection = (AxisSelection)Enum.Parse(typeof(AxisSelection), ((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlConfigurationModel[0].AxisName);
            ((MotionAxisControlPageViewModel)BindingContext).SelectedAxisChangedCommand.Execute(null);
        }
    }

    private void OnSelectedAxisChanged(object sender, EventArgs e)
    {
        if (((MotionAxisControlPageViewModel)BindingContext).SelectedAxis == null)
            return;
        var selectedAxisData = ((MotionAxisControlPageViewModel)BindingContext).SelectedAxis;
        ((MotionAxisControlPageViewModel)BindingContext).MotionAxisControlModel.AxisSelection = (AxisSelection)Enum.Parse(typeof(AxisSelection), selectedAxisData.AxisName);
        ((MotionAxisControlPageViewModel)BindingContext).SelectedAxisChangedCommand.Execute(null);

        ((MotionAxisControlPageViewModel)BindingContext).MovePosRelValue = selectedAxisData.PositionREL_DefaultValue.ToString();
        ((MotionAxisControlPageViewModel)BindingContext).MovePosAbsValue = selectedAxisData.PositionABS_DefaultValue.ToString();
        ((MotionAxisControlPageViewModel)BindingContext).VelPosValue = selectedAxisData.Velocity_DefaultValue.ToString();
        ((MotionAxisControlPageViewModel)BindingContext).AcclPosValue = selectedAxisData.Accel_DefaultValue.ToString();
        ((MotionAxisControlPageViewModel)BindingContext).JerkPosValue = selectedAxisData.Jerk_DefaultValue.ToString();
    }
}