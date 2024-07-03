using ZWave.Enums;
using ZWave.ViewModels.Systems.CausewaySystem;
using ZWave.ViewModels.Systems.ProcessSystem;
using BindingMode = Microsoft.Maui.Controls.BindingMode;

namespace ZWave.Controls;

public partial class AxisControllerWithEntry : ContentView
{
    public AxisControllerWithEntryActions ActionID_Home { get; set; } = AxisControllerWithEntryActions.Home;
    public AxisControllerWithEntryActions ActionID_ArrowContentLeft1 { get; set; } = AxisControllerWithEntryActions.ArrowContentLeft1;
    public AxisControllerWithEntryActions ActionID_ArrowContentLeft2 { get; set; } = AxisControllerWithEntryActions.ArrowContentLeft2;
    public AxisControllerWithEntryActions ActionID_ArrowContentLeft3 { get; set; } = AxisControllerWithEntryActions.ArrowContentLeft3;
    public AxisControllerWithEntryActions ActionID_ArrowContentRight1 { get; set; } = AxisControllerWithEntryActions.ArrowContentRight1;
    public AxisControllerWithEntryActions ActionID_ArrowContentRight2 { get; set; } = AxisControllerWithEntryActions.ArrowContentRight2;
    public AxisControllerWithEntryActions ActionID_ArrowContentRight3 { get; set; } = AxisControllerWithEntryActions.ArrowContentRight3;
    public AxisControllerWithEntryActions ActionID_AxisMoveIcon1 { get; set; } = AxisControllerWithEntryActions.AxisMoveIcon1;
    public AxisControllerWithEntryActions ActionID_AxisMoveIcon2 { get; set; } = AxisControllerWithEntryActions.AxisMoveIcon2;

    public static readonly BindableProperty ActionIDProperty = BindableProperty.Create(nameof(ActionID), typeof(int), typeof(AxisControllerWithEntry), 0, BindingMode.TwoWay);
    public static readonly BindableProperty EntryFieldValueProperty = BindableProperty.Create(nameof(EntryFieldValue), typeof(double), typeof(AxisControllerWithEntry), 0.00, BindingMode.TwoWay, propertyChanged: OnEntryFieldValuePropertyChange);
    public static readonly BindableProperty DisplayEntryFieldValueProperty = BindableProperty.Create(nameof(DisplayEntryFieldValue), typeof(string), typeof(AxisControllerWithEntry), String.Empty, BindingMode.TwoWay, propertyChanged: OnDisplayEntryFieldValuePropertyChange);
    public static readonly BindableProperty IsAllowNegativeInputProperty = BindableProperty.Create(nameof(IsAllowNegativeInput), typeof(bool), typeof(AxisControllerWithEntry), false, propertyChanged: OnIsAllowNegativeInputPropertyChange);
    public static readonly BindableProperty IsIntegerProperty = BindableProperty.Create(nameof(IsInteger), typeof(bool), typeof(AxisControllerWithEntry), false, propertyChanged: OnIsIntegerPropertyChange);
    public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(AxisControllerWithEntry), 0.00, propertyChanged: OnMinValuePropertyChange);
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(AxisControllerWithEntry), 0.00, propertyChanged: OnMaxValuePropertyChange);
    public static readonly BindableProperty AxisTitleProperty = BindableProperty.Create(nameof(AxisTitle), typeof(string), typeof(AxisControllerWithEntry), "X-axis (mm)", propertyChanged: OnAxisTitlePropertyChange);
    public static readonly BindableProperty AxisTypeNameProperty = BindableProperty.Create(nameof(AxisTypeName), typeof(string), typeof(AxisControllerWithEntry), "Donor_Axis_X");
    public static readonly BindableProperty AxisCategoryProperty = BindableProperty.Create(nameof(AxisCategory), typeof(string), typeof(AxisControllerWithEntry), "Donor");
    public static readonly BindableProperty AxisUnitTypeProperty = BindableProperty.Create(nameof(AxisUnitType), typeof(string), typeof(AxisControllerWithEntry), "um", propertyChanged: OnAxisUnitTypePropertyChange);
    public static readonly BindableProperty IsHorizontalAxisProperty = BindableProperty.Create(nameof(IsHorizontalAxis), typeof(bool), typeof(AxisControllerWithEntry), false, propertyChanged: OnIsHorizontalAxisPropertyChange);
    public static readonly BindableProperty IsVerticalAxisProperty = BindableProperty.Create(nameof(IsVerticalAxis), typeof(bool), typeof(AxisControllerWithEntry), false, propertyChanged: OnIsVerticalAxistPropertyChange);
    public static readonly BindableProperty IsAngleAxisProperty = BindableProperty.Create(nameof(IsAngleAxis), typeof(bool), typeof(AxisControllerWithEntry), false, propertyChanged: OnIsAngleAxisPropertyChange);
    public static readonly BindableProperty LeftArrow1ValueProperty = BindableProperty.Create(nameof(LeftArrow1Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);
    public static readonly BindableProperty LeftArrow2ValueProperty = BindableProperty.Create(nameof(LeftArrow2Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);
    public static readonly BindableProperty LeftArrow3ValueProperty = BindableProperty.Create(nameof(LeftArrow3Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);
    public static readonly BindableProperty RightArrow1ValueProperty = BindableProperty.Create(nameof(RightArrow1Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);
    public static readonly BindableProperty RightArrow2ValueProperty = BindableProperty.Create(nameof(RightArrow2Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);
    public static readonly BindableProperty RightArrow3ValueProperty = BindableProperty.Create(nameof(RightArrow3Value), typeof(double), typeof(AxisControllerWithEntry), 0.00);

    public int ActionID
    {
        get => (int)GetValue(ActionIDProperty);
        set => SetValue(ActionIDProperty, value);
    }
    public double EntryFieldValue
    {
        get => (double)GetValue(EntryFieldValueProperty);
        set => SetValue(EntryFieldValueProperty, value);
    }
    public string DisplayEntryFieldValue
    {
        get => (string)GetValue(DisplayEntryFieldValueProperty);
        set => SetValue(DisplayEntryFieldValueProperty, value);
    }
    public bool IsAllowNegativeInput
    {
        get => (bool)GetValue(IsAllowNegativeInputProperty);
        set => SetValue(IsAllowNegativeInputProperty, value);
    }
    public bool IsInteger
    {
        get => (bool)GetValue(IsIntegerProperty);
        set => SetValue(IsIntegerProperty, value);
    }
    public double MinValue
    {
        get => (double)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }
    public double MaxValue
    {
        get => (double)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public string AxisTitle
    {
        get => (string)GetValue(AxisTitleProperty);
        set => SetValue(AxisTitleProperty, value);
    }
    public string AxisTypeName
    {
        get => (string)GetValue(AxisTypeNameProperty);
        set => SetValue(AxisTypeNameProperty, value);
    }
    public string AxisCategory
    {
        get => (string)GetValue(AxisCategoryProperty);
        set => SetValue(AxisCategoryProperty, value);
    }
    public bool IsHorizontalAxis
    {
        get => (bool)GetValue(IsHorizontalAxisProperty);
        set => SetValue(IsHorizontalAxisProperty, value);
    }
    public bool IsVerticalAxis
    {
        get => (bool)GetValue(IsVerticalAxisProperty);
        set => SetValue(IsVerticalAxisProperty, value);
    }
    public bool IsAngleAxis
    {
        get => (bool)GetValue(IsAngleAxisProperty);
        set => SetValue(IsAngleAxisProperty, value);
    }
    public string AxisUnitType
    {
        get => (string)GetValue(AxisUnitTypeProperty);
        set => SetValue(AxisUnitTypeProperty, value);
    }
    public double LeftArrow1Value
    {
        get => (double)GetValue(LeftArrow1ValueProperty);
        set => SetValue(LeftArrow1ValueProperty, value);
    }
    public double LeftArrow2Value
    {
        get => (double)GetValue(LeftArrow2ValueProperty);
        set => SetValue(LeftArrow2ValueProperty, value);
    }
    public double LeftArrow3Value
    {
        get => (double)GetValue(LeftArrow3ValueProperty);
        set => SetValue(LeftArrow3ValueProperty, value);
    }
    public double RightArrow1Value
    {
        get => (double)GetValue(RightArrow1ValueProperty);
        set => SetValue(RightArrow1ValueProperty, value);
    }
    public double RightArrow2Value
    {
        get => (double)GetValue(RightArrow2ValueProperty);
        set => SetValue(RightArrow2ValueProperty, value);
    }
    public double RightArrow3Value
    {
        get => (double)GetValue(RightArrow3ValueProperty);
        set => SetValue(RightArrow3ValueProperty, value);
    }
    public AxisControllerWithEntry()
    {
        InitializeComponent();

        HomeBtn.ActionID = (int)ActionID_Home;
        ArrowContentLeft1.ActionID = (int)ActionID_ArrowContentLeft1;
        ArrowContentLeft2.ActionID = (int)ActionID_ArrowContentLeft2;
        ArrowContentLeft3.ActionID = (int)ActionID_ArrowContentLeft3;
        ArrowContentRight1.ActionID = (int)ActionID_ArrowContentRight1;
        ArrowContentRight2.ActionID = (int)ActionID_ArrowContentRight2;
        ArrowContentRight3.ActionID = (int)ActionID_ArrowContentRight3;
        AxisMoveIcon1.ActionID = (int)ActionID_AxisMoveIcon1;
        AxisMoveIcon2.ActionID = (int)ActionID_AxisMoveIcon2;

        Loaded += OnLoaded;
    }
    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        AxisType.Text = AxisTitle;

        AxisUnitTypeText.Text = AxisUnitType;

        if (IsHorizontalAxis)
        {
            ArrowContentLeft1.ButtonIconSource = "triple_arrow_blue.png";
            ArrowContentLeft2.ButtonIconSource = "double_arrow_blue.png";
            ArrowContentLeft3.ButtonIconSource = "single_arrow_blue.png";
            ArrowContentLeft1.ButtonIconRotation = 180;
            ArrowContentLeft2.ButtonIconRotation = 180;
            ArrowContentLeft3.ButtonIconRotation = 180;
            ArrowContentLeft1.ButtonWidth = 40;
            ArrowContentLeft2.ButtonWidth = 30;
            ArrowContentLeft3.ButtonWidth = 20;

            ArrowContentRight1.ButtonIconSource = "single_arrow_blue.png";
            ArrowContentRight2.ButtonIconSource = "double_arrow_blue.png";
            ArrowContentRight3.ButtonIconSource = "triple_arrow_blue.png";
            ArrowContentRight1.ButtonIconRotation = 0;
            ArrowContentRight2.ButtonIconRotation = 0;
            ArrowContentRight3.ButtonIconRotation = 0;
            ArrowContentRight1.ButtonWidth = 20;
            ArrowContentRight2.ButtonWidth = 30;
            ArrowContentRight3.ButtonWidth = 40;

            AxisMoveIcon1.RotationValue = 180;
            AxisMoveIcon2.RotationValue = 0;
        }
        else if (IsVerticalAxis)
        {
            ArrowContentLeft1.ButtonIconSource = "triple_arrow_blue.png";
            ArrowContentLeft2.ButtonIconSource = "double_arrow_blue.png";
            ArrowContentLeft3.ButtonIconSource = "single_arrow_blue.png";
            ArrowContentLeft1.ButtonIconRotation = 270;
            ArrowContentLeft2.ButtonIconRotation = 270;
            ArrowContentLeft3.ButtonIconRotation = 270;
            ArrowContentLeft1.ButtonWidth = 40;
            ArrowContentLeft2.ButtonWidth = 30;
            ArrowContentLeft3.ButtonWidth = 20;

            ArrowContentRight1.ButtonIconSource = "single_arrow_blue.png";
            ArrowContentRight2.ButtonIconSource = "double_arrow_blue.png";
            ArrowContentRight3.ButtonIconSource = "triple_arrow_blue.png";
            ArrowContentRight1.ButtonIconRotation = 90;
            ArrowContentRight2.ButtonIconRotation = 90;
            ArrowContentRight3.ButtonIconRotation = 90;
            ArrowContentRight1.ButtonWidth = 20;
            ArrowContentRight2.ButtonWidth = 30;
            ArrowContentRight3.ButtonWidth = 40;

            AxisMoveIcon1.RotationValue = 270;
            AxisMoveIcon2.RotationValue = 90;
        }
        else if (IsAngleAxis)
        {
            ArrowContentLeft1.ButtonIconSource = "triple_angle_blue.png";
            ArrowContentLeft2.ButtonIconSource = "double_angle_blue.png";
            ArrowContentLeft3.ButtonIconSource = "single_angle_blue.png";
            ArrowContentLeft1.ButtonIconRotationY = 0;
            ArrowContentLeft2.ButtonIconRotationY = 0;
            ArrowContentLeft3.ButtonIconRotationY = 0;
            ArrowContentLeft1.ButtonWidth = 40;
            ArrowContentLeft2.ButtonWidth = 30;
            ArrowContentLeft3.ButtonWidth = 20;

            ArrowContentRight1.ButtonIconSource = "single_angle_blue.png";
            ArrowContentRight2.ButtonIconSource = "double_angle_blue.png";
            ArrowContentRight3.ButtonIconSource = "triple_angle_blue.png";
            ArrowContentRight1.ButtonIconRotationY = 180;
            ArrowContentRight2.ButtonIconRotationY = 180;
            ArrowContentRight3.ButtonIconRotationY = 180;
            ArrowContentRight1.ButtonWidth = 20;
            ArrowContentRight2.ButtonWidth = 30;
            ArrowContentRight3.ButtonWidth = 40;

            AxisMoveIcon1.RotationValue = 180;
            AxisMoveIcon2.RotationValue = 0;
        }

    }

    private static void OnEntryFieldValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.EntryFieldValue = double.Parse(newValue.ToString());
    }
    private static void OnDisplayEntryFieldValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.DisplayEntryFieldValue = newValue.ToString();
    }
    private static void OnIsAllowNegativeInputPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.IsAllowNegativeInput = (bool)newValue;
    }
    private static void OnIsIntegerPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.IsInteger = (bool)newValue;
    }
    private static void OnMinValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.MinValue = double.Parse(newValue.ToString());
    }
    private static void OnMaxValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisMoveNumberInput.MaxValue = double.Parse(newValue.ToString());
    }

    private static void OnAxisTitlePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisType.Text = (string)newValue;
    }
    private static void OnIsHorizontalAxisPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((bool)newValue)
        {
            ((AxisControllerWithEntry)bindable).ArrowContentLeft1.ButtonIconRotation = 180;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft2.ButtonIconRotation = 180;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft3.ButtonIconRotation = 180;

            ((AxisControllerWithEntry)bindable).ArrowContentRight1.ButtonIconRotation = 0;
            ((AxisControllerWithEntry)bindable).ArrowContentRight2.ButtonIconRotation = 0;
            ((AxisControllerWithEntry)bindable).ArrowContentRight3.ButtonIconRotation = 0;

            ((AxisControllerWithEntry)bindable).AxisMoveIcon1.RotationValue = 180;
            ((AxisControllerWithEntry)bindable).AxisMoveIcon2.RotationValue = 0;
        }
    }
    private static void OnIsVerticalAxistPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((bool)newValue)
        {
            ((AxisControllerWithEntry)bindable).ArrowContentLeft1.ButtonIconRotation = 270;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft2.ButtonIconRotation = 270;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft3.ButtonIconRotation = 270;

            ((AxisControllerWithEntry)bindable).ArrowContentRight1.ButtonIconRotation = 90;
            ((AxisControllerWithEntry)bindable).ArrowContentRight2.ButtonIconRotation = 90;
            ((AxisControllerWithEntry)bindable).ArrowContentRight3.ButtonIconRotation = 90;

            ((AxisControllerWithEntry)bindable).AxisMoveIcon1.RotationValue = 270;
            ((AxisControllerWithEntry)bindable).AxisMoveIcon2.RotationValue = 90;
        }
    }
    private static void OnIsAngleAxisPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((bool)newValue)
        {
            ((AxisControllerWithEntry)bindable).ArrowContentLeft1.ButtonIconRotationY = 0;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft2.ButtonIconRotationY = 0;
            ((AxisControllerWithEntry)bindable).ArrowContentLeft3.ButtonIconRotationY = 0;

            ((AxisControllerWithEntry)bindable).ArrowContentRight1.ButtonIconRotationY = 180;
            ((AxisControllerWithEntry)bindable).ArrowContentRight2.ButtonIconRotationY = 180;
            ((AxisControllerWithEntry)bindable).ArrowContentRight3.ButtonIconRotationY = 180;

            ((AxisControllerWithEntry)bindable).AxisMoveIcon1.RotationValue = 270;
            ((AxisControllerWithEntry)bindable).AxisMoveIcon2.RotationValue = 90;
        }
    }
    private static void OnAxisUnitTypePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((AxisControllerWithEntry)bindable).AxisUnitTypeText.Text = (string)newValue;
    }

    private void OnIconMoveBtnClicked(object sender, EventArgs e)
    {
        ActionID = ((MultipleArrowWithImage)sender).ActionID;
    }
    
    private void OnHomeBtnClicked(object sender, EventArgs e)
    {
        ActionID = ((CustomButtonWithIconText)sender).ActionID;
    }

    private void OnMoveBtnClicked(object sender, EventArgs e)
    {
        ActionID = ((CustomButtonWithIconText)sender).ActionID;
    }

    private void AxisMoveNumberInput_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        EntryFieldValue = ((CustomNumberEntryField)sender).EntryFieldValue;
    }
}