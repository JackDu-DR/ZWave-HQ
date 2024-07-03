using System.ComponentModel;
using ZWave.ViewModels.Systems.ProcessSystem;

namespace ZWave.Controls;

public partial class CustomSliderWithEntry : ContentView
{
    public static readonly BindableProperty IsCheckBoxAppliedProperty = BindableProperty.Create(nameof(IsCheckBoxApplied), typeof(bool), typeof(CustomSliderWithEntry), false);
    public static readonly BindableProperty IsAllowNegativeInputProperty = BindableProperty.Create(nameof(IsAllowNegativeInput), typeof(bool), typeof(CustomSliderWithEntry), false, propertyChanged: OnIsAllowNegativeInputPropertyChange);
    public static readonly BindableProperty IsIntegerProperty = BindableProperty.Create(nameof(IsInteger), typeof(bool), typeof(CustomSliderWithEntry), false);
    public static readonly BindableProperty IsAllowTwoDecimalPlacesProperty = BindableProperty.Create(nameof(IsAllowTwoDecimalPlaces), typeof(bool), typeof(CustomNumberEntryField), true);
    public static readonly BindableProperty IsAllowFourDecimalPlacesProperty = BindableProperty.Create(nameof(IsAllowFourDecimalPlaces), typeof(bool), typeof(CustomSliderWithEntry), false);
    public static readonly BindableProperty SliderIdProperty = BindableProperty.Create(nameof(SliderId), typeof(int), typeof(CustomSliderWithEntry), 1);
    public static readonly BindableProperty SliderLabelNameProperty = BindableProperty.Create(nameof(SliderLabelName), typeof(string), typeof(CustomSliderWithEntry), "", propertyChanged: OnSliderLabelNamePropertyChange);
    public static readonly BindableProperty SliderValueProperty = BindableProperty.Create(nameof(SliderValue), typeof(double), typeof(CustomSliderWithEntry), 0.00, BindingMode.TwoWay, propertyChanged: OnSliderValuePropertyChange);
    public static readonly BindableProperty IsCheckBoxCheckedProperty = BindableProperty.Create(nameof(IsCheckBoxChecked), typeof(bool), typeof(CustomSliderWithEntry), false, BindingMode.TwoWay, propertyChanged: OnIsCheckBoxCheckedPropertyChange);
    public static readonly BindableProperty SliderMinValueProperty = BindableProperty.Create(nameof(SliderMinValue), typeof(double), typeof(CustomSliderWithEntry), 0.00, propertyChanged: OnSliderMinValuePropertyChange);
    public static readonly BindableProperty SliderMaxValueProperty = BindableProperty.Create(nameof(SliderMaxValue), typeof(double), typeof(CustomSliderWithEntry), 99.99, propertyChanged: OnSliderMaxValuePropertyChange);
    public static readonly BindableProperty SliderValueTypeProperty = BindableProperty.Create(nameof(SliderValueType), typeof(string), typeof(CustomSliderWithEntry), "", propertyChanged: OnSliderValueTypeChange);
    public bool IsCheckBoxApplied
    {
        get => (bool)GetValue(IsCheckBoxAppliedProperty);
        set => SetValue(IsCheckBoxAppliedProperty, value);
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
    public bool IsAllowTwoDecimalPlaces
    {
        get => (bool)GetValue(IsAllowTwoDecimalPlacesProperty);
        set => SetValue(IsAllowTwoDecimalPlacesProperty, value);
    }
    public bool IsAllowFourDecimalPlaces
    {
        get => (bool)GetValue(IsAllowFourDecimalPlacesProperty);
        set => SetValue(IsAllowFourDecimalPlacesProperty, value);
    }
    public int SliderId
    {
        get => (int)GetValue(SliderIdProperty);
        set => SetValue(SliderIdProperty, value);
    }
    public string SliderLabelName
    {
        get => (string)GetValue(SliderLabelNameProperty);
        set => SetValue(SliderLabelNameProperty, value);
    }
    public double SliderValue
    {
        get => (double)GetValue(SliderValueProperty);
        set => SetValue(SliderValueProperty, value);
    }
    public bool IsCheckBoxChecked
    {
        get => (bool)GetValue(IsCheckBoxCheckedProperty);
        set => SetValue(IsCheckBoxCheckedProperty, value);
    }
    public double SliderMinValue
    {
        get => (double)GetValue(SliderMinValueProperty);
        set => SetValue(SliderMinValueProperty, value);
    }
    public double SliderMaxValue
    {
        get => (double)GetValue(SliderMaxValueProperty);
        set => SetValue(SliderMaxValueProperty, value);
    }
    public string SliderValueType
    {
        get => (string)GetValue(SliderValueTypeProperty);
        set => SetValue(SliderValueTypeProperty, value);
    }

    private double _sliderValue;
    private double _variableValue;
    private CancellationTokenSource _cancellationTokenSource;

    public CustomSliderWithEntry()
    {
        InitializeComponent();
        Loaded += OnLoaded;

        _cancellationTokenSource = new CancellationTokenSource();
    }
    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        SliderLabelText.Text = SliderLabelName + " (" + SliderMinValue + " - " + SliderMaxValue + " " + SliderValueType + ")";
        SliderValueText.Value = SliderValue;
        SliderValueText.Minimum = SliderMinValue;
        SliderValueText.Maximum = SliderMaxValue;
        SliderEntry.IsEnabled = (!IsCheckBoxApplied || IsCheckBoxChecked);
        SliderEntry.MinValue = SliderMinValue;
        SliderEntry.MaxValue = SliderMaxValue;
        SliderEntry.IsInteger = IsInteger;
        SliderEntry.IsAllowTwoDecimalPlaces = IsAllowTwoDecimalPlaces;
        SliderEntry.IsAllowFourDecimalPlaces = IsAllowFourDecimalPlaces;
        SliderValueText.IsEnabled = (!IsCheckBoxApplied || IsCheckBoxChecked);
        SliderEntry.EntryFieldValue = SliderValue;
        SliderEntry.DisplayEntryFieldValue = SliderValue.ToString();
        CustomCheckBox.IsChecked = IsCheckBoxChecked;

        if (!IsCheckBoxApplied)
            CustomCheckBox.IsVisible = false;
    }
    private static void OnIsAllowNegativeInputPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderEntry.IsAllowNegativeInput = (bool)newValue;
    }
    private static void OnSliderLabelNamePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderLabelText.Text = (string)newValue;
    }
    private static void OnSliderValuePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderValueText.Value = (double)newValue;
        ((CustomSliderWithEntry)bindable).SliderEntry.EntryFieldValue = (double)newValue;

        string oldValueStr = oldValue.ToString();
        string oldValueRemoveLastStr = oldValueStr.Substring(0, oldValueStr.Length - 1);
        double checkingValue = oldValueRemoveLastStr == "" || oldValueRemoveLastStr == "-" ? 0 : double.Parse(oldValueRemoveLastStr);

        if (checkingValue != (double)newValue)
            ((CustomSliderWithEntry)bindable).SliderEntry.DisplayEntryFieldValue = newValue.ToString();
    }
    private static void OnIsCheckBoxCheckedPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).CustomCheckBox.IsChecked = (bool)newValue;
        ((CustomSliderWithEntry)bindable).SliderValueText.IsEnabled = (bool)newValue;
        ((CustomSliderWithEntry)bindable).SliderEntry.IsEnabled = (bool)newValue;
    }
    private static void OnSliderMinValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderLabelText.Text = ((CustomSliderWithEntry)bindable).SliderLabelName + " (" + newValue + " - " + ((CustomSliderWithEntry)bindable).SliderMaxValue + " " + ((CustomSliderWithEntry)bindable).SliderValueType + ")";
        ((CustomSliderWithEntry)bindable).SliderValueText.Minimum = (double)newValue;
        ((CustomSliderWithEntry)bindable).SliderEntry.MinValue = (double)newValue;
    }
    private static void OnSliderMaxValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderLabelText.Text = ((CustomSliderWithEntry)bindable).SliderLabelName + " (" + ((CustomSliderWithEntry)bindable).SliderMinValue + " - " + newValue + " " + ((CustomSliderWithEntry)bindable).SliderValueType + ")";
        ((CustomSliderWithEntry)bindable).SliderValueText.Maximum = (double)newValue;
        ((CustomSliderWithEntry)bindable).SliderEntry.MaxValue = (double)newValue;
    }
    private static void OnSliderValueTypeChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomSliderWithEntry)bindable).SliderLabelText.Text = ((CustomSliderWithEntry)bindable).SliderLabelName + " (" + ((CustomSliderWithEntry)bindable).SliderMinValue + " - " + ((CustomSliderWithEntry)bindable).SliderMaxValue + " " + newValue + ")";
    }

    private void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        IsCheckBoxChecked = e.Value;
    }

    public void onSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (IsInteger && Convert.ToInt32(e.OldValue) == e.NewValue)
            return;

        if (!IsInteger && Math.Round(e.OldValue, 2) == e.NewValue)
            return;

        var receivedValue = e.NewValue;
        if (IsInteger)
        {
            int getValue = Convert.ToInt32(e.NewValue);
            receivedValue = getValue;
        }
        else if (IsAllowTwoDecimalPlaces)
        {
            double getValue = Math.Round(e.NewValue, 2);
            string getValue2 = getValue.ToString("0.##");
            receivedValue = double.Parse(getValue2);
        }
        else if (IsAllowFourDecimalPlaces)
        {
            double getValue = Math.Round(e.NewValue, 4);
            string getValue2 = getValue.ToString("0.####");
            receivedValue = double.Parse(getValue2);
        }
        SliderValue = receivedValue;
    }

    private void SliderEntry_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "EntryFieldValue")
        {
            SliderValue = ((CustomNumberEntryField)sender).EntryFieldValue;
        }
    }
}
