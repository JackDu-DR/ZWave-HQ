using ZWave.Helpers;

namespace ZWave.Controls;

public partial class CustomNumberEntryField : ContentView
{
    public static readonly BindableProperty EntryFieldValueProperty = BindableProperty.Create(nameof(EntryFieldValue), typeof(double), typeof(CustomNumberEntryField), 0.00, BindingMode.TwoWay);
    public static readonly BindableProperty DisplayEntryFieldValueProperty = BindableProperty.Create(nameof(DisplayEntryFieldValue), typeof(string), typeof(CustomNumberEntryField), String.Empty, BindingMode.TwoWay, propertyChanged: OnDisplayEntryFieldValuePropertyChange);
    public static readonly BindableProperty IsIntegerProperty = BindableProperty.Create(nameof(IsInteger), typeof(bool), typeof(CustomNumberEntryField), false);
    public static readonly BindableProperty IsAllowTwoDecimalPlacesProperty = BindableProperty.Create(nameof(IsAllowTwoDecimalPlaces), typeof(bool), typeof(CustomNumberEntryField), true);
    public static readonly BindableProperty IsAllowFourDecimalPlacesProperty = BindableProperty.Create(nameof(IsAllowFourDecimalPlaces), typeof(bool), typeof(CustomNumberEntryField), false);
    public static readonly BindableProperty IsBoundaryAppliedProperty = BindableProperty.Create(nameof(IsBoundaryApplied), typeof(bool), typeof(CustomNumberEntryField), true);
    public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(CustomNumberEntryField), 0.00);
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(CustomNumberEntryField), 0.00);
    public static readonly BindableProperty FieldTotalWidthProperty = BindableProperty.Create(nameof(FieldTotalWidth), typeof(int), typeof(CustomNumberEntryField), 0, propertyChanged: OnFieldTotalWidthPropertyChange);
    public static readonly BindableProperty IsAllowNegativeInputProperty = BindableProperty.Create(nameof(IsAllowNegativeInput), typeof(bool), typeof(CustomNumberEntryField), false);

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
    public bool IsAllowFourDecimalPlaces
    {
        get => (bool)GetValue(IsAllowFourDecimalPlacesProperty);
        set => SetValue(IsAllowFourDecimalPlacesProperty, value);
    }
    public bool IsAllowTwoDecimalPlaces
    {
        get => (bool)GetValue(IsAllowTwoDecimalPlacesProperty);
        set => SetValue(IsAllowTwoDecimalPlacesProperty, value);
    }
    public bool IsInteger
    {
        get => (bool)GetValue(IsIntegerProperty);
        set => SetValue(IsIntegerProperty, value);
    }
    public bool IsBoundaryApplied
    {
        get => (bool)GetValue(IsBoundaryAppliedProperty);
        set => SetValue(IsBoundaryAppliedProperty, value);
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
    public int FieldTotalWidth
    {
        get => (int)GetValue(FieldTotalWidthProperty);
        set => SetValue(FieldTotalWidthProperty, value);
    }
    public bool IsAllowNegativeInput
    {
        get => (bool)GetValue(IsAllowNegativeInputProperty);
        set => SetValue(IsAllowNegativeInputProperty, value);
    }

    //private CancellationTokenSource _cancellationTokenSource;

    //private System.Threading.Timer timer;
    //private readonly TimeSpan typingDelay = TimeSpan.FromSeconds(1);

    public CustomNumberEntryField()
    {
        InitializeComponent();
        Loaded += OnLoaded;

        EntryValue.WidthRequest = FieldTotalWidth;
        //_cancellationTokenSource = new CancellationTokenSource();
    }
    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
    }
    private static void OnDisplayEntryFieldValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomNumberEntryField)bindable).EntryValue.Text = newValue.ToString();
    }
    private static void OnFieldTotalWidthPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomNumberEntryField)bindable).EntryContainer.WidthRequest = (int)newValue + 1;
        ((CustomNumberEntryField)bindable).EntryValue.WidthRequest = (int)newValue;
    }
    private async void OnEntryValueChanged(object sender, TextChangedEventArgs e)
    {
        double newValue = MinValue;
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            return;
        }
        else if (e.NewTextValue.EndsWith("."))
        {
            return;
        }
        else if (!InputHelper.IsValidNumericInput(e.NewTextValue))
        {
            EntryValue.Text = (MinValue).ToString();
            EntryFieldValue = MinValue;
        }
        else if (IsAllowNegativeInput == false && InputHelper.IsValidNegativeNumberInput(e.NewTextValue))
        {
            EntryValue.Text = (MinValue).ToString();
            EntryFieldValue = MinValue;
        }
        //else if ((((IsAllowNegativeInput == false && InputHelper.IsValidNumberInput(e.NewTextValue))) || (IsAllowNegativeInput == true && (InputHelper.IsValidNegativeNumberInput(e.NewTextValue) || InputHelper.IsValidNegativeNumberInput(e.NewTextValue)))))
        else
        {
            if ((IsAllowNegativeInput == false && !InputHelper.IsValidNumberInput(e.NewTextValue)) || (IsAllowNegativeInput == true && !InputHelper.IsValidNumberInput(e.NewTextValue) && !InputHelper.IsValidNegativeNumberInput(e.NewTextValue)))
            {
                int existingDot = e.NewTextValue.ToCharArray().Count(v => v == '.');
                if (existingDot > 1)
                {
                    string overrideValue = e.NewTextValue;

                    int firstDotIndex = overrideValue.IndexOf('.');
                    int secondDotIndex = overrideValue.IndexOf('.', firstDotIndex + 1);

                    if (secondDotIndex != -1)
                    {
                        overrideValue = overrideValue.Substring(0, secondDotIndex);
                    }

                    EntryValue.Text = overrideValue;
                    EntryFieldValue = double.Parse(overrideValue);
                }
                return;
            }

            newValue = double.Parse(e.NewTextValue);

            double receivedValue = newValue;
            if (IsInteger)
            {
                int getValue = Convert.ToInt32(newValue);
                receivedValue = getValue;
            }
            else if (IsAllowTwoDecimalPlaces)
            {
                double getValue = Math.Round(newValue, 2);
                string getValue2 = getValue.ToString("0.##");
                receivedValue = double.Parse(getValue2);
            }
            else if (IsAllowFourDecimalPlaces)
            {
                double getValue = Math.Round(newValue, 4);
                string getValue2 = getValue.ToString("0.####");
                receivedValue = double.Parse(getValue2);
            }

            if (newValue != receivedValue)
                EntryValue.Text = (receivedValue).ToString();

            double passValue = receivedValue;
            if (IsBoundaryApplied && MaxValue < newValue && newValue != 0)
            {
                passValue = MaxValue;
                EntryFieldValue = passValue;
                await Task.Delay(500);
                if(e.NewTextValue == EntryValue.Text)
                    EntryValue.Text = (MaxValue).ToString();
            }
            else if (IsBoundaryApplied && newValue < MinValue && newValue != 0)
            {
                passValue = MinValue;
                EntryFieldValue = passValue;
                await Task.Delay(500);
                if (e.NewTextValue == EntryValue.Text)
                    EntryValue.Text = (MinValue).ToString();
            }
            else
            {
                EntryFieldValue = passValue;
            }

            //timer?.Dispose();
            //timer = new System.Threading.Timer(_ =>
            //{
            //EntryValue.Text = (passValue).ToString();
            //DisplayEntryFieldValue = passValue;
            //EntryFieldValue = passValue;

            //}, null, typingDelay, TimeSpan.FromMilliseconds(-1));
        }
    }
}