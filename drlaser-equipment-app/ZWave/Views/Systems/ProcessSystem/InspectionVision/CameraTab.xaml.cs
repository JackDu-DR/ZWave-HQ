using System.ComponentModel;
using ZWave.Controls;
using ZWave.Enums;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Views.Systems.ProcessSystem.InspectionVision;

public partial class CameraTab : ContentView
{
    public static readonly BindableProperty CameraNameProperty = BindableProperty.Create(nameof(CameraName), typeof(string), typeof(CameraTab), string.Empty, propertyChanged: OnCameraNamePropertyChange);

    private CancellationTokenSource _cancellationTokenSource;
    public string CameraName
    {
        get => (string)GetValue(CameraNameProperty);
        set => SetValue(CameraNameProperty, value);
    }

    public CustomSliderWithEntryLibrary SliderID_ExposureTime { get; set; } = CustomSliderWithEntryLibrary.ExposureTime;
    public CustomSliderWithEntryLibrary SliderID_Gain { get; set; } = CustomSliderWithEntryLibrary.Gain;
    public CustomSliderWithEntryLibrary SliderID_Gamma { get; set; } = CustomSliderWithEntryLibrary.Gamma;
    public CustomSliderWithEntryLibrary SliderID_Sharpness { get; set; } = CustomSliderWithEntryLibrary.Sharpness;
    public CustomSliderWithEntryLibrary SliderID_BlackLevel { get; set; } = CustomSliderWithEntryLibrary.BlackLevel;
    public CustomSliderWithEntryLibrary SliderID_Phi { get; set; } = CustomSliderWithEntryLibrary.Phi;

    public CameraTab()
    {
        InitializeComponent();

        ExposureTimeSlider.SliderId = (int)SliderID_ExposureTime;
        GainSlider.SliderId = (int)SliderID_Gain;
        GammaSlider.SliderId = (int)SliderID_Gamma;
        SharpnessSlider.SliderId = (int)SliderID_Sharpness;
        BlackLevelSlider.SliderId = (int)SliderID_BlackLevel;

        Loaded += OnLoaded;

        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        OnAllContentValueChange();
    }

    private static void OnCameraNamePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((CameraTab)bindable).OnAllContentValueChange();
    }

    private void OnAllContentValueChange()
    {
        var inspectionVisionSelectedCamera = ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera;

        SharpnessSlider.IsVisible = inspectionVisionSelectedCamera.ShowSharpness;
        if (inspectionVisionSelectedCamera.ShowSharpness)
        {
            SharpnessSlider.IsAllowNegativeInput = inspectionVisionSelectedCamera.Sharpness_Min < 0 ? true : false;
            SharpnessSlider.SliderMinValue = inspectionVisionSelectedCamera.Sharpness_Min;
            SharpnessSlider.SliderMaxValue = inspectionVisionSelectedCamera.Sharpness_Max;
            SharpnessSlider.SliderValueType = inspectionVisionSelectedCamera.Sharpness_ValueType;
        }

        ExposureTimeSlider.IsAllowNegativeInput = inspectionVisionSelectedCamera.ExposureTime_Min < 0 ? true : false;
        GainSlider.IsAllowNegativeInput = inspectionVisionSelectedCamera.Gain_Min < 0 ? true : false;
        GammaSlider.IsAllowNegativeInput = inspectionVisionSelectedCamera.Gamma_Min < 0 ? true : false;
        BlackLevelSlider.IsAllowNegativeInput = inspectionVisionSelectedCamera.BlackLevel_Min < 0 ? true : false;

        ExposureTimeSlider.SliderMinValue = inspectionVisionSelectedCamera.ExposureTime_Min;
        GainSlider.SliderMinValue = inspectionVisionSelectedCamera.Gain_Min;
        GammaSlider.SliderMinValue = inspectionVisionSelectedCamera.Gamma_Min;
        BlackLevelSlider.SliderMinValue = inspectionVisionSelectedCamera.BlackLevel_Min;

        ExposureTimeSlider.SliderMaxValue = inspectionVisionSelectedCamera.ExposureTime_Max;
        GainSlider.SliderMaxValue = inspectionVisionSelectedCamera.Gain_Max;
        GammaSlider.SliderMaxValue = inspectionVisionSelectedCamera.Gamma_Max;
        BlackLevelSlider.SliderMaxValue = inspectionVisionSelectedCamera.BlackLevel_Max;

        ExposureTimeSlider.SliderValueType = inspectionVisionSelectedCamera.ExposureTime_ValueType;
        GainSlider.SliderValueType = inspectionVisionSelectedCamera.Gain_ValueType;
        GammaSlider.SliderValueType = inspectionVisionSelectedCamera.Gamma_ValueType;
        BlackLevelSlider.SliderValueType = inspectionVisionSelectedCamera.BlackLevel_ValueType;
    }

    private void SliderPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var currentSliderEnumValue = (CustomSliderWithEntryLibrary)Enum.Parse(typeof(CustomSliderWithEntryLibrary), ((CustomSliderWithEntry)sender).SliderId.ToString());

        if (e.PropertyName == "IsCheckBoxChecked")
        {
            switch (currentSliderEnumValue)
            {
                case CustomSliderWithEntryLibrary.Gamma:
                    ((InspectionVisionPageViewModel)BindingContext).GammaEnableValueChangedCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }
        else if (e.PropertyName == "SliderValue" && ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.IsCameraLive)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                await Task.Delay(1000, cancellationToken);
                if (!cancellationToken.IsCancellationRequested)
                {
                    switch (currentSliderEnumValue)
                    {
                        case CustomSliderWithEntryLibrary.ExposureTime:
                            ((InspectionVisionPageViewModel)BindingContext).ExposureValueChangedCommand.Execute(null);
                            break;
                        case CustomSliderWithEntryLibrary.Gain:
                            ((InspectionVisionPageViewModel)BindingContext).GainValueChangedCommand.Execute(null);
                            break;
                        case CustomSliderWithEntryLibrary.Gamma:
                            ((InspectionVisionPageViewModel)BindingContext).GammaValueChangedCommand.Execute(null);
                            break;
                        case CustomSliderWithEntryLibrary.BlackLevel:
                            ((InspectionVisionPageViewModel)BindingContext).BlackLevelValueChangedCommand.Execute(null);
                            break;
                        case CustomSliderWithEntryLibrary.Sharpness:
                            if (((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera.ShowSharpness)
                            {
                                ((InspectionVisionPageViewModel)BindingContext).SharpnessValueChangedCommand.Execute(null);
                                break;
                            }
                            else
                                break;

                        default:
                            break;
                    }
                }
            }, cancellationToken);
        }
    }
}