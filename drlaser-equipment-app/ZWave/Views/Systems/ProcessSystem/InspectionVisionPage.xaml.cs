using CommonLib.Enums;
using System.Diagnostics;
using ZWave.Controls.Graphic;
using ZWave.Enums;
using ZWave.Helpers;
using ZWave.Models;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Views.Systems.ProcessSystem;

public partial class InspectionVisionPage : ContentView
{
    private InspectionVisionPageViewModel _inspectionVisionPageViewModel;
    public InspectionVisionPage()
    {
        InitializeComponent();

        //ROIRadioBtn.IsChecked = true;
        _inspectionVisionPageViewModel = new InspectionVisionPageViewModel();

#if WINDOWS
        AndroidInspectionContent.Children.Clear();
#elif ANDROID
        WindowsInspectionContent.Children.Clear();
#endif
    }

    //static double _scrollX;
    //static double _scrollY;
    //private void LiveCameraScrollView_Scrolled(object sender, ScrolledEventArgs e)
    //{
    //    ((InspectionVisionPageViewModel)BindingContext).DiscardROICommand.Execute(null);
    //    //ShapeGraphicView?.ClearShape();

    //    scrollTimer.Stop();
    //    scrollTimer.Start();

    //    _scrollX = e.ScrollX;
    //    _scrollY = e.ScrollY;
    //}

    private void ShapeGraphicView_Scrolled(object sender, ScrolledEventArgs e)
    {
        _inspectionVisionPageViewModel.DiscardROICommand.Execute(null);
    }

    private void ShapeGraphicView_EndScrolled(object sender, Point e)
    {
        if (((InspectionVisionPageViewModel)BindingContext).IsCameraChanging)
            return;

        ((InspectionVisionPageViewModel)BindingContext).IsDiscardButtonEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsApplyButtonEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).Scroll(e.X, e.Y);
    }

    private void ShapeGraphicView_PointChanged(object sender, Point e)
    {
        ((InspectionVisionPageViewModel)BindingContext).MousePosition = e;
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

    private void OnShapeChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

        try
        {
            ShapeType result = (ShapeType)Enum.Parse(typeof(ShapeType), selectedPickerContentName);
            ((InspectionVisionPageViewModel)BindingContext).ShapeType = result;
        }
        catch (ArgumentException)
        {
            Debug.WriteLine("Unable to convert the string to enum.");
        }
        //_inspectionVisionPage.ClearShapes();
        //ShapeGraphicView?.ClearShape();
        ClearShapes();
    }

    private void ClearShapes()
    {
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(10), () =>
        {
            if (ShapeGraphicView != null)
            {
                ShapeGraphicView.ClearShapes();
            }
        });
    }

    private void OnModelIDChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

        ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.CalibrationModelId = selectedPickerContentName;

    }

    //private void OnScrollTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    //{
    //    ((InspectionVisionPageViewModel)BindingContext).Scroll(_scrollX, _scrollY);
    //}

    private async void OnCameraChange(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.CameraSelect = (CameraSelect)Enum.Parse(typeof(CameraSelect), ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera.CameraName.ToString());

        var inspectionVisionSelectedCamera = ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera;

        ((InspectionVisionPageViewModel)BindingContext).IsCameraChanging = true;

        ((InspectionVisionPageViewModel)BindingContext).FullImageWidth = inspectionVisionSelectedCamera.CameraMaxSize_X;
        ((InspectionVisionPageViewModel)BindingContext).FullImageHeight = inspectionVisionSelectedCamera.CameraMaxSize_Y;

        ((InspectionVisionPageViewModel)BindingContext).ScrollX = inspectionVisionSelectedCamera.CameraMaxSize_X / 2 - 250;
        ((InspectionVisionPageViewModel)BindingContext).ScrollY = inspectionVisionSelectedCamera.CameraMaxSize_Y / 2 - 250;
        ((InspectionVisionPageViewModel)BindingContext).ScrollValueChanged(inspectionVisionSelectedCamera.CameraMaxSize_X / 2 - 250, inspectionVisionSelectedCamera.CameraMaxSize_Y / 2 - 250);

        //await Task.Delay(1000);
        ((InspectionVisionPageViewModel)BindingContext).IsCameraChanging = false;

#if WINDOWS
                CameraTab_Windows.CameraName = ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera.CameraName.ToString();

#elif ANDROID
                CameraTab_Android.CameraName = ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.SelectedCamera.CameraName.ToString();
#endif
    }
    private void onZoomInBtnClicked(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).ZoomInCommand.Execute(null);
    }

    private void onZoomOutBtnClicked(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).ZoomOutCommand.Execute(null);
    }

    private void onZoomResetBtnClicked(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).ZoomResetCommand.Execute(null);
    }


    private void onSaveBtnClicked(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).SaveCommand.Execute(null);
    }
    private void OnDiscardButtonClicked(object sender, EventArgs e)
    {
        //await Task.Delay(100);
        //ShapeGraphicView.ClearShape();
        ClearShapes();
    }

    private void OnCameraTabClicked(object sender, TappedEventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).IsCameraPageBtnEnabled = true;
        TabTitleLabel_Camera.TextColor = ResourceHelper.GetColor("GreyNeutral14");
        TabActiveLabel_Camera.IsVisible = true;

        TabTitleLabel_Teaching.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Teaching.IsVisible = false;
        TabTitleLabel_Calibration.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Calibration.IsVisible = false;

        ((InspectionVisionPageViewModel)BindingContext).IsTeachPageBtnEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsCalibrationPageBtnEnabled = false;

        ((InspectionVisionPageViewModel)BindingContext).IsDrawingEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsTeaching = false;

        ClearShapes();
    }
    private void OnTeachingTabClicked(object sender, TappedEventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).IsTeachPageBtnEnabled = true;
        TabTitleLabel_Teaching.TextColor = ResourceHelper.GetColor("GreyNeutral14");
        TabActiveLabel_Teaching.IsVisible = true;

        TabTitleLabel_Camera.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Camera.IsVisible = false;
        TabTitleLabel_Calibration.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Calibration.IsVisible = false;


        ((InspectionVisionPageViewModel)BindingContext).IsCameraPageBtnEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsCalibrationPageBtnEnabled = false;

        ((InspectionVisionPageViewModel)BindingContext).IsDrawingEnabled = ((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.IsCameraLive ? true : false;
        ((InspectionVisionPageViewModel)BindingContext).IsTeaching = false;
    }
    private void OnCalibrationTabClicked(object sender, TappedEventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).IsCalibrationPageBtnEnabled = true;
        TabTitleLabel_Calibration.TextColor = ResourceHelper.GetColor("GreyNeutral14");
        TabActiveLabel_Calibration.IsVisible = true;

        TabTitleLabel_Teaching.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Teaching.IsVisible = false;

        TabTitleLabel_Camera.TextColor = ResourceHelper.GetColor("GreyNeutral11");
        TabActiveLabel_Camera.IsVisible = false;


        ((InspectionVisionPageViewModel)BindingContext).IsCameraPageBtnEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsTeachPageBtnEnabled = false;

        ((InspectionVisionPageViewModel)BindingContext).IsDrawingEnabled = false;
        ((InspectionVisionPageViewModel)BindingContext).IsTeaching = false;

        ClearShapes();
    }

    private void ConnectBtn_Clicked(object sender, EventArgs e)
    {
        if (((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.IsCameraConnected)
            ((InspectionVisionPageViewModel)BindingContext).DisconnectCommand.Execute(null);
        else
            ((InspectionVisionPageViewModel)BindingContext).ConnectCommand.Execute(null);
    }
    private void LiveBtn_Clicked(object sender, EventArgs e)
    {
        if (((InspectionVisionPageViewModel)BindingContext).InspectionVisionModel.IsCameraLive)
            ((InspectionVisionPageViewModel)BindingContext).OffLiveCommand.Execute(null);
        else
            ((InspectionVisionPageViewModel)BindingContext).LiveCommand.Execute(null);
    }
    private void TriggerBtn_Clicked(object sender, EventArgs e)
    {
        ((InspectionVisionPageViewModel)BindingContext).TriggerCommand.Execute(null);
    }
}

