using System.Windows.Input;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class ButtonInGroup : VerticalStackLayout
{
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ButtonInGroup), false, propertyChanged: OnIsSelectedPropertyChange);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ButtonInGroup), string.Empty, propertyChanged: OnTextPropertyChange);
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ButtonInGroup), null, propertyChanged: OnImageSourcePropertyChange);
    public static readonly BindableProperty OnClickedCommandProperty = BindableProperty.Create(nameof(OnClickedCommand), typeof(ICommand), typeof(ButtonInGroup), default, propertyChanged: OnClickedPropertyChanged);

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public ICommand OnClickedCommand
    {
        get => (ICommand)GetValue(OnClickedCommandProperty);
        set => SetValue(OnClickedCommandProperty, value);
    }

    public object CommandParameter { get; set; }

    /// <summary>
    /// Do not use this handler when this control is a part of HorizontalButtonGroup
    /// </summary>
    public event EventHandler OnButtonSelected;

    public ButtonInGroup()
	{
		InitializeComponent();
        Button.Clicked += OnButtonClick;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        Button.TextColor = ResourceHelper.GetColor("Black");
        UpdateButtonState(this, IsSelected);
    }

    private void OnButtonClick(object sender, EventArgs e)
    {
        if (!IsSelected)
        {
            OnClickedCommand?.Execute(CommandParameter);
            OnButtonSelected?.Invoke(this, e);
            IsSelected = true;
        }
    }

    private static void OnIsSelectedPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var container = (ButtonInGroup)bindable;
        UpdateButtonState(container, (bool)newValue);
    }

    private static void OnTextPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((ButtonInGroup)bindable).Button.Text = (string)newValue;
    }

    private static void OnImageSourcePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((ButtonInGroup)bindable).Button.ImageSource = (ImageSource)newValue;
    }

    private static void UpdateButtonState(ButtonInGroup buttonContainer, bool isSelected)
    {
        buttonContainer.IsSelected = isSelected;
        buttonContainer.SelectedButtonUnderline.IsVisible = isSelected;
        var styleName = isSelected ? "SelectedHorizontalButtonStyle" : "DefaultHorizontalButtonStyle";
        buttonContainer.Button.CustomStyle = ResourceHelper.GetStyles(styleName);
    }

    private static void OnClickedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ButtonInGroup)bindable).OnClickedCommand = (ICommand)newValue;
    }
}