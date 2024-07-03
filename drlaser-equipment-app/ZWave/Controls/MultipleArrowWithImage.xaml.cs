
using System;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class MultipleArrowWithImage : Frame
{
    private bool IsPointerOver;
    public event EventHandler Clicked;

    public static readonly BindableProperty ActionIDProperty = BindableProperty.Create(nameof(ActionID), typeof(int), typeof(MultipleArrowWithImage), 1);
    public static readonly BindableProperty ButtonIconSourceProperty = BindableProperty.Create(nameof(ButtonIconSource), typeof(string), typeof(MultipleArrowWithImage), "single_arrow_blue.png", propertyChanged: OnButtonIconSourcePropertyChange);
    public static readonly BindableProperty ButtonWidthProperty = BindableProperty.Create(nameof(ButtonWidth), typeof(int), typeof(MultipleArrowWithImage), 20, propertyChanged: OnButtonWidthPropertyChange);
    public static readonly BindableProperty ButtonIconRotationProperty = BindableProperty.Create(nameof(ButtonIconRotation), typeof(int), typeof(MultipleArrowWithImage), 0, propertyChanged: OnButtonIconRotationPropertyChange);
    public static readonly BindableProperty ButtonIconRotationYProperty = BindableProperty.Create(nameof(ButtonIconRotationY), typeof(int), typeof(MultipleArrowWithImage), 0, propertyChanged: OnButtonIconRotationYPropertyChange);

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

    public int ButtonWidth
    {
        get => (int)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }
    public int ButtonIconRotation
    {
        get => (int)GetValue(ButtonIconRotationProperty);
        set => SetValue(ButtonIconRotationProperty, value);
    }

    public int ButtonIconRotationY
    {
        get => (int)GetValue(ButtonIconRotationYProperty);
        set => SetValue(ButtonIconRotationYProperty, value);
    }
    public Style CustomStyle { get; set; }

    public MultipleArrowWithImage()
    {
        InitializeComponent();
        FrameContainer.Style = CustomStyle ?? ResourceHelper.GetStyles("DefaultArrowButtonStyle");

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

        ButtonIconImage.Source = ButtonIconSource;
        ButtonIconImage.WidthRequest = ButtonWidth;

        if (ButtonIconRotation != 0)
            ButtonIconContainer.Rotation = ButtonIconRotation;

        if (ButtonIconRotationY != 0)
            ButtonIconContainer.RotationY = ButtonIconRotationY;

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
        ((MultipleArrowWithImage)bindable).ButtonIconImage.Source = (string)newValue;
    }
    private static void OnButtonWidthPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((MultipleArrowWithImage)bindable).ButtonIconImage.WidthRequest = (int)newValue;
    }
    private static void OnButtonIconRotationPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((int)newValue != 0)
            ((MultipleArrowWithImage)bindable).ButtonIconContainer.Rotation = (int)newValue;
    }
    private static void OnButtonIconRotationYPropertyChange(BindableObject bindable, object _, object newValue)
    {
        if ((int)newValue != 0)
            ((MultipleArrowWithImage)bindable).ButtonIconContainer.RotationY = (int)newValue;
    }
}