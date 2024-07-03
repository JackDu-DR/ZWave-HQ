namespace ZWave.Controls;

public partial class StatusLabel : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(StatusLabel), string.Empty, propertyChanged: OnTitlePropertyChange);
    public static readonly BindableProperty StatusColorProperty = BindableProperty.Create(nameof(StatusColor), typeof(Color), typeof(StatusLabel), Colors.White, propertyChanged: OnStatusChanged);
    public static readonly BindableProperty StatusTextProperty = BindableProperty.Create(nameof(StatusText), typeof(string), typeof(StatusLabel), string.Empty, propertyChanged: OnStatusTextChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Color StatusColor
    {
        get => (Color)GetValue(StatusColorProperty);
        set => SetValue(StatusColorProperty, value);
    }

    public string StatusText
    {
        get => (string)GetValue(StatusTextProperty);
        set => SetValue(StatusTextProperty, value);
    }

    public StatusLabel()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        StatusTextLabel.Text = StatusText;
        Border.BackgroundColor = StatusColor;
    }

    private static void OnTitlePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((StatusLabel)bindable).TitleLabel.Text = (string)newValue;
    }

    private static void OnStatusChanged(BindableObject bindable, object _, object newValue)
    {
        var statusLabel = (StatusLabel)bindable;
        statusLabel.Border.BackgroundColor = (Color)newValue;
    }

    private static void OnStatusTextChanged(BindableObject bindable, object _, object newValue)
    {
        var statusLabel = (StatusLabel)bindable;
        statusLabel.StatusTextLabel.Text = (string)newValue;
    }
}