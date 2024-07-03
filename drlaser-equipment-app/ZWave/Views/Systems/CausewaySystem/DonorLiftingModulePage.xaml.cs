using CommonLib.Enums;
using System.Diagnostics;
using ZWave.Controls;
using ZWave.Enums;
using ZWave.ViewModels.Systems.CausewaySystem;

namespace ZWave.Views.Systems.CausewaySystem;

public partial class DonorLiftingModulePage : ContentView
{
    public DonorLiftingModulePage()
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
        bool loadCompleted = ((DonorLiftingModulePageViewModel)BindingContext).LoadCompleted;
        var configurationData = ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleConfigurationModel;

        await Task.Run(() =>
        {
            if (loadCompleted)
            {
                configurationData = ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleConfigurationModel;
            }
            else
            {
                while (!loadCompleted)
                {
                    loadCompleted = ((DonorLiftingModulePageViewModel)BindingContext).LoadCompleted;
                    if (loadCompleted)
                    {
                        configurationData = ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleConfigurationModel;
                    }
                }
            }
        });

        for (int i = 0; i < configurationData.Count; i++)
        {
            if (configurationData[i].AxisName == "Donor_Axis_X")
            {
                Axis_X_Windows.AxisTypeName = "Donor_Axis_X";
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

                Axis_X_Android.AxisTypeName = "Donor_Axis_X";
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
            else if (configurationData[i].AxisName == "Donor_Axis_Y")
            {
                Axis_Y_Windows.AxisTypeName = "Donor_Axis_Y";
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

                Axis_Y_Android.AxisTypeName = "Donor_Axis_Y";
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
            else if (configurationData[i].AxisName == "Donor_Axis_Z")
            {
                Axis_Z_Windows.AxisTypeName = "Donor_Axis_Z";
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

                Axis_Z_Android.AxisTypeName = "Donor_Axis_Z";
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
        }
    }
    private void OnLoadingBtnClicked(object sender, EventArgs e)
    {
        ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleModel.DonorLifterUIElement = DonorLifterUIElement.Donor_Loading;
        ((DonorLiftingModulePageViewModel)BindingContext).LoadingUnloadingCommand.Execute(null);
    }
    private void OnUnloadingBtnClicked(object sender, EventArgs e)
    {
        ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleModel.DonorLifterUIElement = DonorLifterUIElement.Donor_Unloading;
        ((DonorLiftingModulePageViewModel)BindingContext).LoadingUnloadingCommand.Execute(null);
    }

    private void AxisControllerWithEntry_PropertyChanged(object sender, EventArgs e)
    {
        int actionID = ((AxisControllerWithEntry)sender).ActionID;
        var senderValue = ((AxisControllerWithEntry)sender);
        if (actionID != 0)
        {
            var bindingModelName = ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleModel;
            var bindingName = ((DonorLiftingModulePageViewModel)BindingContext);

            bindingModelName.DonorLifterUIElement = (DonorLifterUIElement)Enum.Parse(typeof(DonorLifterUIElement), senderValue.AxisTypeName);

            switch (actionID)
            {
                case (int)AxisControllerWithEntryActions.Home:
                    bindingModelName.MotionCmd = MotionCmd.home;
                    bindingName.HomeCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft1:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.left: MoveDirection.up; 
                    bindingModelName.MoveRel = senderValue.LeftArrow1Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft2:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.left: MoveDirection.up; 
                    bindingModelName.MoveRel = senderValue.LeftArrow2Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentLeft3:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.left: MoveDirection.up; 
                    bindingModelName.MoveRel = senderValue.LeftArrow3Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight1:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.right : MoveDirection.down;
                    bindingModelName.MoveRel = senderValue.RightArrow1Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight2:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.right : MoveDirection.down;
                    bindingModelName.MoveRel = senderValue.RightArrow2Value;
                    bindingName.MoveCommand.Execute(null);
                    break;

                case (int)AxisControllerWithEntryActions.ArrowContentRight3:
                    bindingModelName.MotionCmd = MotionCmd.move_rel;

                    bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.right : MoveDirection.down;
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
                        bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.left : MoveDirection.up;
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
                        bindingModelName.MoveDirection = senderValue.IsHorizontalAxis ? MoveDirection.right : MoveDirection.down;
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

    //    ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleModel.DonorLifterUIElement = "Donor_Camera_Change";
    //    ((DonorLiftingModulePageViewModel)BindingContext).DonorLiftingModuleModel.CameraSelect = selectedPickerContentName;
    //    ((DonorLiftingModulePageViewModel)BindingContext).CameraChangeCommand.Execute(null);
    //}
}