using Microsoft.Maui.Layouts;
using System.Windows.Input;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class CustomButton : Frame
{
    private bool IsPointerOver;
    private bool IsTapped = false; // used to fix android bug only

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CustomButton), false, propertyChanged: OnIsSelectedPropertyChanged);
    public static readonly BindableProperty NormalBackgroundColorProperty = BindableProperty.Create(nameof(NormalBackgroundColor), typeof(Color), typeof(CustomButton), Colors.Transparent);
    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(CustomButton), Colors.Transparent);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton), string.Empty, propertyChanged: OnTextPropertyChanged);
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CustomButton), null, propertyChanged: OnImageSourceChanged);
    public static readonly BindableProperty CustomStyleProperty = BindableProperty.Create(nameof(CustomStyle), typeof(Style), typeof(CustomButton), null, propertyChanged: OnCustomStyleChanged);
    public static readonly BindableProperty OnClickedCommandProperty = BindableProperty.Create(nameof(OnClickedCommand), typeof(ICommand), typeof(ButtonInGroup), default, propertyChanged: OnClickedPropertyChanged);

    public event EventHandler Clicked;

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public Color NormalBackgroundColor
    {
        get => (Color)GetValue(NormalBackgroundColorProperty);
        set => SetValue(NormalBackgroundColorProperty, value);
    }

    public Color SelectedBackgroundColor
    {
        get => (Color)GetValue(SelectedBackgroundColorProperty);
        set => SetValue(SelectedBackgroundColorProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public FlexJustify JustifyContent { get; set; } = FlexJustify.Center;
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public Color TextColor { get; set; } = Colors.Black;

    /// <summary>
    /// Set space between contents is not valid if JustifyContent = SpaceBetween
    /// because this already set the maximum space between contents
    /// </summary>
    public Button.ButtonContentLayout ContentLayout { get; set; }
    public Style CustomStyle
    {
        get => (Style)GetValue(CustomStyleProperty);
        set => SetValue(CustomStyleProperty, value);
    }

    public ICommand OnClickedCommand
    {
        get => (ICommand)GetValue(OnClickedCommandProperty);
        set => SetValue(OnClickedCommandProperty, value);
    }

    public CustomButton()
    {
        InitializeComponent();
        Loaded += OnLoaded;
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

    public void OnTapped(object sender, EventArgs e)
    {
#if ANDROID
        // even though NumberOfTapsRequired set to 1, this function still trigger two time in android
        if (IsTapped)
        {
            IsTapped = false;
            return;
        }
        IsTapped = true;
#endif
        VisualStateManager.GoToState(FrameContainer, "Pressed");
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), ChangeVisualState);

        OnClickedCommand?.Execute(sender);
        Clicked?.Invoke(sender, EventArgs.Empty);
    }

    private new void ChangeVisualState()
    {
        VisualStateManager.GoToState(FrameContainer, IsPointerOver ? "PointerOver" : "Normal");
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        FrameContainer.Style = CustomStyle ?? ResourceHelper.GetStyles ("DefaultCustomButtonStyle");
        FlexContainer.JustifyContent = JustifyContent;

        if (ContentLayout != null)
        {
            switch (ContentLayout.Position)
            {
                case Button.ButtonContentLayout.ImagePosition.Top:
                    if (JustifyContent != FlexJustify.SpaceBetween)
                    {
                        ImageContainer.Margin = new Thickness(0, 0, 0, ContentLayout.Spacing);
                    }
                    FlexContainer.Direction = FlexDirection.ColumnReverse;
                    break;
                case Button.ButtonContentLayout.ImagePosition.Right:
                    if (JustifyContent != FlexJustify.SpaceBetween)
                    {
                        ImageContainer.Margin = new Thickness(ContentLayout.Spacing, 0, 0, 0);
                    }
                    FlexContainer.Direction = FlexDirection.Row;
                    break;
                case Button.ButtonContentLayout.ImagePosition.Bottom:
                    if (JustifyContent != FlexJustify.SpaceBetween)
                    {
                        ImageContainer.Margin = new Thickness(0, ContentLayout.Spacing, 0, 0);
                    }
                    FlexContainer.Direction = FlexDirection.Column;
                    break;
                case Button.ButtonContentLayout.ImagePosition.Left:
                default:
                    if (JustifyContent != FlexJustify.SpaceBetween)
                    {
                        ImageContainer.Margin = new Thickness(0, 0, ContentLayout.Spacing, 0);
                    }
                    FlexContainer.Direction = FlexDirection.RowReverse;
                    break;
            }
        }

        if (!string.IsNullOrEmpty(Text))
        {
            ButtonText.TextColor = TextColor;
        }
        else
        {
            ButtonText.IsVisible = false;
        }

        ButtonImage.IsVisible = ImageSource != null;
        FrameContainer.BackgroundColor = IsSelected ? SelectedBackgroundColor : NormalBackgroundColor;

        Loaded -= OnLoaded;
    }

    private static void OnIsSelectedPropertyChanged(BindableObject bindable, object _, object newValue)
    {
        if ((bool)newValue)
            ((CustomButton)bindable).FrameContainer.BackgroundColor = ((CustomButton)bindable).SelectedBackgroundColor;
        else
            ((CustomButton)bindable).FrameContainer.BackgroundColor = ((CustomButton)bindable).NormalBackgroundColor;
    }

    private static void OnTextPropertyChanged(BindableObject bindable, object _, object newValue)
    {
        var buttonText = ((CustomButton)bindable).ButtonText;
        buttonText.IsVisible = !string.IsNullOrEmpty((string)newValue);
    }

    private static void OnImageSourceChanged(BindableObject bindable, object _, object newValue)
    {
        var buttonImage = ((CustomButton)bindable).ButtonImage;
        buttonImage.IsVisible = newValue != null;
    }

    private static void OnCustomStyleChanged(BindableObject bindable, object _, object newValue)
    {
        ((CustomButton)bindable).FrameContainer.Style = (Style)newValue;
    }

    private static void OnClickedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((CustomButton)bindable).OnClickedCommand = (ICommand)newValue;
    }
}