using ZWave.Helpers;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Views.Systems.ProcessSystem.InspectionVision;

public partial class CalibrationTab : ContentView
{
	public CalibrationTab()
	{
		InitializeComponent();
	}

    private void OnModelIDChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

        ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.CalibrationModelId = selectedPickerContentName;

    }
}