using CommonLib.Enums;
using System.Diagnostics;
using ZWave.Enums;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Views.Systems.ProcessSystem.InspectionVision;

public partial class TeachingTab : ContentView
{
    public CustomSliderWithEntryLibrary SliderID_NumLevels { get; set; } = CustomSliderWithEntryLibrary.NumLevels;
    public CustomSliderWithEntryLibrary SliderID_AngStart { get; set; } = CustomSliderWithEntryLibrary.AngStart;
    public CustomSliderWithEntryLibrary SliderID_AngExtent { get; set; } = CustomSliderWithEntryLibrary.AngExtent;
    public CustomSliderWithEntryLibrary SliderID_AngStep { get; set; } = CustomSliderWithEntryLibrary.AngStep;
    public TeachingTab()
	{
		InitializeComponent();

        NumLevelsSlider.SliderId = (int)SliderID_NumLevels;
        AngStartSlider.SliderId = (int)SliderID_AngStart;
        AngExtentSlider.SliderId = (int)SliderID_AngExtent;
        AngStepSlider.SliderId = (int)SliderID_AngStep;
    }

    //private void OnDrawShapeTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    var radioButton = (RadioButton)sender;

    //    if (radioButton.IsChecked)
    //    {
    //        ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.DrawShapeType = (DrawShapeCategory)Enum.Parse(typeof(DrawShapeCategory), (string)radioButton.Value);
    //        if ((string)radioButton.Value == "0")
    //        {
    //            ((InspectionVisionPageViewModel)BindingContext).IsDrawROIShape = true;
    //            ((InspectionVisionPageViewModel)BindingContext).IsDrawSearchRegionShape = false;
    //        }
    //        else if ((string)radioButton.Value == "1")
    //        {
    //            ((InspectionVisionPageViewModel)BindingContext).IsDrawROIShape = false;
    //            ((InspectionVisionPageViewModel)BindingContext).IsDrawSearchRegionShape = true;
    //        }
    //    }
    //}

    private void OnModelChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

        try
        {
            ModelSelect result = (ModelSelect)Enum.Parse(typeof(ModelSelect), selectedPickerContentName);
            ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.ModelSelection = result;

        }
        catch (ArgumentException)
        {
            Debug.WriteLine("Unable to convert the string to enum.");
        }
    }

    private void OnMetricChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

        try
        {
            MetricSelect result = (MetricSelect)Enum.Parse(typeof(MetricSelect), selectedPickerContentName);
            ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.MetricSelection = result;

        }
        catch (ArgumentException)
        {
            Debug.WriteLine("Unable to convert the string to enum.");
        }
    }
}