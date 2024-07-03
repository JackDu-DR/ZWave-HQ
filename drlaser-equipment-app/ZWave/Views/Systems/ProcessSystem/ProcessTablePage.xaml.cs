using CommonLib.Enums;
using System.Diagnostics;
using ZWave.Controls;
using ZWave.Enums;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Views.Systems.ProcessSystem;

public partial class ProcessTablePage : ContentView
{
    public ProcessTablePage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        LoadDefaultDataAfterConfigurationDone();
    }

    private async void LoadDefaultDataAfterConfigurationDone()
    {
        bool loadCompleted = ((ProcessTablePageViewModel)BindingContext).LoadCompleted;
        var configurationData = ((ProcessTablePageViewModel)BindingContext).ProcessTableConfigurationModel;

        await Task.Run(() =>
        {
            if (loadCompleted)
            {
                configurationData = ((ProcessTablePageViewModel)BindingContext).ProcessTableConfigurationModel;
            }
            else
            {
                while (!loadCompleted)
                {
                    loadCompleted = ((ProcessTablePageViewModel)BindingContext).LoadCompleted;
                    if (loadCompleted)
                    {
                        configurationData = ((ProcessTablePageViewModel)BindingContext).ProcessTableConfigurationModel;
                    }
                }
            }
        });

        for (int i = 0; i < configurationData.Count; i++)
        {
            if (configurationData[i].AxisName == "Pro_Table_Axis_X")
            {
                Axis_X_Windows.AxisTypeName = "Pro_Table_Axis_X";
                Axis_X_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_X_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_X_Windows.IsInteger = false;
                Axis_X_Windows.EntryFieldValue = 0;
                Axis_X_Windows.DisplayEntryFieldValue = "0";
                Axis_X_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_X_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_X_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_X_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_X_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_X_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_X_Android.AxisTypeName = "Pro_Table_Axis_X";
                Axis_X_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_X_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_X_Android.IsInteger = false;
                Axis_X_Android.EntryFieldValue = 0;
                Axis_X_Android.DisplayEntryFieldValue = "0";
                Axis_X_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_X_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_X_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_X_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_X_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_X_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

            }
            else if (configurationData[i].AxisName == "Pro_Table_Axis_Y")
            {
                Axis_Y_Windows.AxisTypeName = "Pro_Table_Axis_Y";
                Axis_Y_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_Y_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Y_Windows.IsInteger = false;
                Axis_Y_Windows.EntryFieldValue = 0;
                Axis_Y_Windows.DisplayEntryFieldValue = "0";
                Axis_Y_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Y_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Y_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Y_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Y_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Y_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_Y_Android.AxisTypeName = "Pro_Table_Axis_Y";
                Axis_Y_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_Y_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Y_Android.IsInteger = false;
                Axis_Y_Android.EntryFieldValue = 0;
                Axis_Y_Android.DisplayEntryFieldValue = "0";
                Axis_Y_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Y_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Y_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Y_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Y_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Y_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;
            }
            else if (configurationData[i].AxisName == "Pro_Table_Tip_Tilt_Z")
            {
                Axis_Z_Windows.AxisTypeName = "Pro_Table_Tip_Tilt_Z";
                Axis_Z_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_Z_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Z_Windows.IsInteger = false;
                Axis_Z_Windows.EntryFieldValue = 0;
                Axis_Z_Windows.DisplayEntryFieldValue = "0";
                Axis_Z_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Z_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Z_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Z_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Z_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Z_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_Z_Android.AxisTypeName = "Pro_Table_Tip_Tilt_Z";
                Axis_Z_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_Z_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Z_Android.IsInteger = false;
                Axis_Z_Android.EntryFieldValue = 0;
                Axis_Z_Android.DisplayEntryFieldValue = "0";
                Axis_Z_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Z_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Z_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Z_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Z_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Z_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;
            }
            else if (configurationData[i].AxisName == "Pro_Table_Tip_Tilt_TX")
            {
                Axis_X0_Windows.AxisTypeName = "Pro_Table_Tip_Tilt_TX";
                Axis_X0_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_X0_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_X0_Windows.IsInteger = false;
                Axis_X0_Windows.EntryFieldValue = 0;
                Axis_X0_Windows.DisplayEntryFieldValue = "0";
                Axis_X0_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_X0_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_X0_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_X0_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_X0_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_X0_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_X0_Android.AxisTypeName = "Pro_Table_Tip_Tilt_TX";
                Axis_X0_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_X0_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_X0_Android.IsInteger = false;
                Axis_X0_Android.EntryFieldValue = 0;
                Axis_X0_Android.DisplayEntryFieldValue = "0";
                Axis_X0_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_X0_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_X0_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_X0_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_X0_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_X0_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

            }
            else if (configurationData[i].AxisName == "Pro_Table_Tip_Tilt_TY")
            {
                Axis_Y0_Windows.AxisTypeName = "Pro_Table_Tip_Tilt_TY";
                Axis_Y0_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_Y0_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Y0_Windows.IsInteger = false;
                Axis_Y0_Windows.EntryFieldValue = 0;
                Axis_Y0_Windows.DisplayEntryFieldValue = "0";
                Axis_Y0_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Y0_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Y0_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Y0_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Y0_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Y0_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_Y0_Android.AxisTypeName = "Pro_Table_Tip_Tilt_TY";
                Axis_Y0_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_Y0_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Y0_Android.IsInteger = false;
                Axis_Y0_Android.EntryFieldValue = 0;
                Axis_Y0_Android.DisplayEntryFieldValue = "0";
                Axis_Y0_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Y0_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Y0_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Y0_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Y0_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Y0_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;
            }
            else if (configurationData[i].AxisName == "Pro_Table_Tip_Tilt_T")
            {
                Axis_Angle_Windows.AxisTypeName = "Pro_Table_Tip_Tilt_T";
                Axis_Angle_Windows.MinValue = configurationData[i].EntryValue_Min;
                Axis_Angle_Windows.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Angle_Windows.IsInteger = false;
                Axis_Angle_Windows.EntryFieldValue = 0;
                Axis_Angle_Windows.DisplayEntryFieldValue = "0";
                Axis_Angle_Windows.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Angle_Windows.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Angle_Windows.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Angle_Windows.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Angle_Windows.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Angle_Windows.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;

                Axis_Angle_Android.AxisTypeName = "Pro_Table_Tip_Tilt_T";
                Axis_Angle_Android.MinValue = configurationData[i].EntryValue_Min;
                Axis_Angle_Android.MaxValue = configurationData[i].EntryValue_Max;
                Axis_Angle_Android.IsInteger = false;
                Axis_Angle_Android.EntryFieldValue = 0;
                Axis_Angle_Android.DisplayEntryFieldValue = "0";
                Axis_Angle_Android.LeftArrow1Value = configurationData[i].ArrowContentLeft1DefaultValue;
                Axis_Angle_Android.LeftArrow2Value = configurationData[i].ArrowContentLeft2DefaultValue;
                Axis_Angle_Android.LeftArrow3Value = configurationData[i].ArrowContentLeft3DefaultValue;
                Axis_Angle_Android.RightArrow1Value = configurationData[i].ArrowContentRight1DefaultValue;
                Axis_Angle_Android.RightArrow2Value = configurationData[i].ArrowContentRight2DefaultValue;
                Axis_Angle_Android.RightArrow3Value = configurationData[i].ArrowContentRight3DefaultValue;
            }
        }
    }

    private void OnLoadingBtnClicked(object sender, EventArgs e)
    {
        ((ProcessTablePageViewModel)BindingContext).ProcessTableModel.ProTableUIElement = ProTableUIElement.Pro_Table_Loading;
        ((ProcessTablePageViewModel)BindingContext).LoadingUnloadingCommand.Execute(null);
    }
    private void OnUnloadingBtnClicked(object sender, EventArgs e)
    {
        ((ProcessTablePageViewModel)BindingContext).ProcessTableModel.ProTableUIElement = ProTableUIElement.Pro_Table_Unloading;
        ((ProcessTablePageViewModel)BindingContext).LoadingUnloadingCommand.Execute(null);
    }

    private void AxisControllerWithEntry_PropertyChanged(object sender, EventArgs e)
    {
        int actionID = ((AxisControllerWithEntry)sender).ActionID;
        var senderValue = ((AxisControllerWithEntry)sender);
        if (actionID != 0)
        {
            var bindingModelName = ((ProcessTablePageViewModel)BindingContext).ProcessTableModel;
            var bindingName = ((ProcessTablePageViewModel)BindingContext);

            bindingModelName.ProTableUIElement = (ProTableUIElement)Enum.Parse(typeof(ProTableUIElement), senderValue.AxisTypeName);

            switch (actionID)
            {
                case (int)AxisControllerWithEntryActions.Home:
                    bindingModelName.MotionCmd = MotionCmd.home; //home
                    bindingName.HomeCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft1:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.left : MoveDirection.up;
                    bindingModelName.MoveRel = senderValue.LeftArrow1Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft2:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.left : MoveDirection.up;
                    bindingModelName.MoveRel = senderValue.LeftArrow2Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft3:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.left : MoveDirection.up;
                    bindingModelName.MoveRel = senderValue.LeftArrow3Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight1:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.right : MoveDirection.down;
                    bindingModelName.MoveRel = senderValue.RightArrow1Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight2:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.right : MoveDirection.down;
                    bindingModelName.MoveRel = senderValue.RightArrow2Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight3:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.right : MoveDirection.down;
                    bindingModelName.MoveRel = senderValue.RightArrow3Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.AxisMoveIcon1:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    string receivedInputString = senderValue.EntryFieldValue.ToString();
                    if (receivedInputString == "" || receivedInputString == null || double.Parse(receivedInputString) == 0)
                        return;

                    var currentValue = double.Parse(receivedInputString);

                    if (0 <= currentValue)
                    {
                        double micronVal = currentValue / 1000;
                        bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.left : MoveDirection.up;
                        bindingModelName.MoveRel = micronVal;
                        bindingName.MoveCommand.Execute(null);
                        break;
                    }
                    else
                        break;

                case (int)AxisControllerWithEntryActions.AxisMoveIcon2:
                    bindingModelName.MotionCmd = MotionCmd.move_rel; 

                    string inputString = senderValue.EntryFieldValue.ToString();
                    if (inputString == "" || inputString == null || double.Parse(inputString) == 0)
                        return;

                    var newValue = double.Parse(inputString);

                    if (0 <= newValue)
                    {
                        double micronVal = newValue / 1000;
                        bindingModelName.MoveDirection = senderValue.IsHorizontalAxis || senderValue.IsAngleAxis ? MoveDirection.right : MoveDirection.down;
                        bindingModelName.MoveRel = micronVal;
                        bindingName.MoveCommand.Execute(null);
                        break;
                    }
                    else
                        break;
            }
                ((AxisControllerWithEntry)sender).ActionID = 0;
        }
    }

    //private void onCameraChange(object sender, EventArgs e)
    //{
    //    var picker = (Picker)sender;
    //    int selectedIndex = picker.SelectedIndex;
    //    var selectedPickerContentName = (selectedIndex != -1) ? picker.Items[selectedIndex] : "";

    //    ((ProcessTablePageViewModel)BindingContext).ProcessTableModel.ProTableUIElement = "Pro_Table_Camera_Change";
    //    ((ProcessTablePageViewModel)BindingContext).ProcessTableModel.CameraSelect = selectedPickerContentName;
    //    ((ProcessTablePageViewModel)BindingContext).CameraChangeCommand.Execute(null);
    //}
}