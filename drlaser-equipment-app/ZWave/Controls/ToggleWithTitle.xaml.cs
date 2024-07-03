using ZWave.Enums;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class ToggleWithTitle : Grid
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ToggleWithTitle), string.Empty, propertyChanged: OnTitlePropertyChange);
    public static readonly BindableProperty ToggleOnTextProperty = BindableProperty.Create(nameof(ToggleOnText), typeof(string), typeof(ToggleWithTitle), "On", propertyChanged: ToggleOnTextPropertyChange);
    public static readonly BindableProperty ToggleOffTextProperty = BindableProperty.Create(nameof(ToggleOffText), typeof(string), typeof(ToggleWithTitle), "Off", propertyChanged: ToggleOffTextPropertyChange);
    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleWithTitle), false, Microsoft.Maui.Controls.BindingMode.TwoWay, propertyChanged: OnIsToggledPropertyChange);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public string ToggleOnText
    {
        get => (string)GetValue(ToggleOnTextProperty);
        set => SetValue(ToggleOnTextProperty, value);
    }
    public string ToggleOffText
    {
        get => (string)GetValue(ToggleOffTextProperty);
        set => SetValue(ToggleOffTextProperty, value);
    }

    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }

    public ToggleType ToggleType { get; set; } = ToggleType.RightSideText;
    public double TitleWidthRequest { get; set; }
    public bool IsTitleWidthAutoSize { get; set; }

    public ToggleWithTitle()
	{
		InitializeComponent();
#if WINDOWS
        // Hiden default label text of switch on windows
        Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("NoLabel", (handler, View) =>
        {
            handler.PlatformView.OnContent = null;
            handler.PlatformView.OffContent = null;
            handler.PlatformView.MinWidth = 0;
        });
#endif
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        ToggleButton.IsToggled = IsToggled;
        if (!IsTitleWidthAutoSize)
        {
            ToggleContainer.ColumnDefinitions[0].Width = TitleWidthRequest;
        }
        ToggleButton.Toggled += OnToggled;
        SetupToggleView();
        UpdateToggleButton(ToggleButton, ToggleButton.IsToggled);
    }

    private void SetupToggleView()
    {
        switch (ToggleType)
        {
            case ToggleType.NoText:
                ToggleLeftLabel.IsVisible = false;
                ToggleRightLabel.IsVisible = false;
                break;
            case ToggleType.LeftSideText:
                ToggleRightLabel.IsVisible = false;
                break;
            case ToggleType.RightSideText:
                ToggleLeftLabel.IsVisible = false;
#if WINDOWS
                ToggleButton.Margin = new Thickness(0, -2, 0, 0);
#endif
                break;
            case ToggleType.BothSideText:
            default:
                ToggleLeftLabel.Text = ToggleOffText;
                ToggleRightLabel.Text = ToggleOnText;
                break;
        }
    }

    public event EventHandler ToggleChanged;
    private void OnToggled(object sender, ToggledEventArgs e)
    {
        UpdateToggleButton((Switch)sender, e.Value);
        IsToggled = e.Value;
        ToggleChanged?.Invoke(this, e);
    }

    private void UpdateToggleButton(Switch ToggleButton, bool isToggled)
    {
        switch (ToggleType)
        {
            case ToggleType.BothSideText:
                UpdateBothSideTextButton(isToggled, ToggleLeftLabel, ToggleRightLabel);
                break;
            case ToggleType.LeftSideText:
                ToggleLeftLabel.Text = isToggled ? ToggleOnText : ToggleOffText;
                break;
            case ToggleType.RightSideText:
            default:
                ToggleRightLabel.Text = isToggled ? ToggleOnText : ToggleOffText;
                break;
        }
        ToggleButton.ThumbColor = isToggled ? ResourceHelper.GetColor("White") : ResourceHelper.GetColor("GreyNeutral11");
    }

    private static void UpdateBothSideTextButton(bool isToggled, Label toggleLeftLabel, Label toggleRightLabel)
    {
        if (isToggled)
        {
            toggleLeftLabel.TextColor = ResourceHelper.GetColor("GreyNeutral9");
            toggleRightLabel.TextColor = ResourceHelper.GetColor("Black");
        }
        else
        {
            toggleLeftLabel.TextColor = ResourceHelper.GetColor("Black");
            toggleRightLabel.TextColor = ResourceHelper.GetColor("GreyNeutral9");
        }
    }

    private static void OnTitlePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((ToggleWithTitle)bindable).ToggleTitle.Text = (string)newValue;
    }

    private static void ToggleOnTextPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var toggle = (ToggleWithTitle)bindable;
        var isToggleLefLabelVisible = toggle.ToggleLeftLabel.IsVisible;
        var isToggleRightLabelVisible = toggle.ToggleRightLabel.IsVisible;
        if (isToggleLefLabelVisible && isToggleRightLabelVisible)
        {
            toggle.ToggleRightLabel.Text = (string)newValue;
        }
        else if (toggle.ToggleButton.IsToggled)
        {
            if (isToggleLefLabelVisible)
            {
                toggle.ToggleLeftLabel.Text = (string)newValue;
            }
            else
            {
                toggle.ToggleRightLabel.Text = (string)newValue;
            }
        }
    }
    private static void ToggleOffTextPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var toggle = (ToggleWithTitle)bindable;
        var isToggleLefLabelVisible = toggle.ToggleLeftLabel.IsVisible;
        var isToggleRightLabelVisible = toggle.ToggleRightLabel.IsVisible;
        if (isToggleLefLabelVisible && isToggleRightLabelVisible)
        {
            toggle.ToggleLeftLabel.Text = (string)newValue;
        }
        else if (!toggle.ToggleButton.IsToggled)
        {
            if (isToggleLefLabelVisible)
            {
                toggle.ToggleLeftLabel.Text = (string)newValue;
            }
            else
            {
                toggle.ToggleRightLabel.Text = (string)newValue;
            }
        }
    }

    private static void OnIsToggledPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var toggleButton = ((ToggleWithTitle)bindable).ToggleButton;
        if (toggleButton.IsToggled != (bool)newValue)
        {
            toggleButton.IsToggled = (bool)newValue;
        }
    }
}