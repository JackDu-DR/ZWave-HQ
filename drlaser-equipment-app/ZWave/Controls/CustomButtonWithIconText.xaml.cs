using System;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class CustomButtonWithIconText : Frame
{
    private bool IsPointerOver;
    public event EventHandler Clicked;

    public static readonly BindableProperty ActionIDProperty = BindableProperty.Create(nameof(ActionID), typeof(int), typeof(CustomButtonWithIconText), 1);
    public static readonly BindableProperty ButtonIconSourceProperty = BindableProperty.Create(nameof(ButtonIconSource), typeof(string), typeof(CustomButtonWithIconText), "", propertyChanged: OnButtonIconSourcePropertyChange);
    public static readonly BindableProperty ButtonIconMarginTopProperty = BindableProperty.Create(nameof(ButtonIconMarginTop), typeof(int), typeof(CustomButtonWithIconText), 0, propertyChanged: OnButtonIconMarginTopPropertyChange);
    public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(CustomButtonWithIconText), "", propertyChanged: OnButtonLabelTextPropertyChange);
    public static readonly BindableProperty ButtonIconWidthProperty = BindableProperty.Create(nameof(ButtonIconWidth), typeof(int), typeof(CustomButtonWithIconText), 30, propertyChanged: OnButtonIconWidthPropertyChange);
    public static readonly BindableProperty ButtonWidthProperty = BindableProperty.Create(nameof(ButtonWidth), typeof(int), typeof(CustomButtonWithIconText), 50, propertyChanged: OnButtonWidthPropertyChange);
    public static readonly BindableProperty RotationValueProperty = BindableProperty.Create(nameof(RotationValue), typeof(int), typeof(CustomButtonWithIconText), 0, propertyChanged: OnRotationValuePropertyChange);
    public static readonly BindableProperty IsShowBtnProperty = BindableProperty.Create(nameof(IsShowBtn), typeof(bool), typeof(CustomButtonWithIconText), true, propertyChanged: OnIsShowBtnPropertyChange);

    public Style CustomStyle { get; set; }

    public int ActionID
    {
        get => (int)GetValue(ActionIDProperty);
        set => SetValue(ActionIDProperty, value);
    }
    public string ButtonIconSource
    {
        get => (string)GetValue(ButtonIconSourceProperty);
        set => SetValue(ButtonIconSourceProperty, value);
    }
    public int ButtonIconMarginTop
    {
        get => (int)GetValue(ButtonIconMarginTopProperty);
        set => SetValue(ButtonIconMarginTopProperty, value);
    }
    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }
    public int ButtonIconWidth
    {
        get => (int)GetValue(ButtonIconWidthProperty);
        set => SetValue(ButtonIconWidthProperty, value);
    }
    public int ButtonWidth
    {
        get => (int)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }
    public int RotationValue
    {
        get => (int)GetValue(RotationValueProperty);
        set => SetValue(RotationValueProperty, value);
    }
    public bool IsShowBtn
    {
        get => (bool)GetValue(IsShowBtnProperty);
        set => SetValue(IsShowBtnProperty, value);
    }

    public CustomButtonWithIconText()
    {
        InitializeComponent();
        FrameContainer.Style = CustomStyle ?? ResourceHelper.GetStyles("DefaultCustomButtonIconTextStyle");

        Loaded += OnLoaded;
    }
    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += (sender, e) =>
        {
            VisualStateManager.GoToState(FrameContainer, "Pressed");
            Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), ChangeVisualState);
            OnClicked();
        };
        GestureRecognizers.Add(tapGestureRecognizer);

        ButtonIconImage.IsVisible = ButtonIconSource == "" ? false : true;
        ButtonIconImage.Source = ButtonIconSource;
        ButtonIconImage.WidthRequest = ButtonIconWidth;
        ButtonContainer.WidthRequest = ButtonWidth;

        if (ButtonText == "")
            CustomButtonText.IsVisible = false;

        if (ButtonText != "")
            CustomButtonText.Text = ButtonText;

        if (ButtonIconMarginTop != 0)
            ButtonIconImage.Margin = new Thickness(0, ButtonIconMarginTop, 0, 0);

        ButtonIconImage.Rotation = RotationValue;

        FrameContainer.IsVisible = IsShowBtn;
    }
    public void OnPointerEntered(object sender, PointerEventArgs e)
    {
        IsPointerOver = true;
        ChangeVisualState();
    }

    public void OnPointerExited(object sender, PointerEventArgs e)
    {
        IsPointerOver = false;
        ChangeVisualState();
    }
    private new void ChangeVisualState()
    {
        VisualStateManager.GoToState(FrameContainer, IsPointerOver ? "PointerOver" : "Normal");
    }

    protected virtual void OnClicked()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }

    private static void OnButtonIconSourcePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).ButtonIconImage.Source = (string)newValue;
    }
    private static void OnButtonIconMarginTopPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).ButtonIconImage.Margin = new Thickness(0, (int)newValue, 0, 0);
    }
    private static void OnButtonLabelTextPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((string)newValue == "")
            ((CustomButtonWithIconText)bindable).CustomButtonText.IsVisible = false;

        if ((string)newValue != "")
            ((CustomButtonWithIconText)bindable).CustomButtonText.Text = (string)newValue;
    }
    private static void OnButtonIconWidthPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).ButtonIconImage.WidthRequest = (int)newValue;
    }
    private static void OnButtonWidthPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).ButtonContainer.WidthRequest = (int)newValue;
    }
    private static void OnRotationValuePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).ButtonIconImage.Rotation = (int)newValue;
    }
    private static void OnIsShowBtnPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((CustomButtonWithIconText)bindable).FrameContainer.IsVisible = (bool)newValue;
    }
}
