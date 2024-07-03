namespace ZWave.Controls;

public partial class DotStatusWithTitle : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(DotStatusWithTitle), string.Empty, propertyChanged: OnTitlePropertyChange);
    public static readonly BindableProperty StatusColorProperty = BindableProperty.Create(nameof(StatusColor), typeof(Color), typeof(DotStatusWithTitle), Colors.Green, propertyChanged: OnStatusColorPropertyChange);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Color StatusColor
    {
        get => (Color)GetValue(StatusColorProperty);
        set 
        {
            var isColorChange = StatusColor != value;
            SetValue(StatusColorProperty, value);
            if (isColorChange)
            {
                OnStatusChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public double DotSize { get; set; } = 12;

    public FlowDirection HorizontalDirection { get; set; } = FlowDirection.LeftToRight;

    public double ItemsSpacing { get; set; } = 16;

    public FontAttributes TitleFontAttributes { get; set; } = FontAttributes.None;

    public event EventHandler OnStatusChange;

    public DotStatusWithTitle()
	{
		InitializeComponent();
        Loaded += OnLoaded;
	}

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        SetupItemsDirection();
        StatusGrid.ColumnSpacing = ItemsSpacing;
        StatusLabel.Text = Title;
        StatusLabel.FontAttributes = TitleFontAttributes;
        StatusDot.WidthRequest = DotSize;
        StatusDot.HeightRequest = DotSize;
        StatusDot.Fill = StatusColor;
    }

    private void SetupItemsDirection()
    {
        switch (HorizontalDirection)
        {
            case FlowDirection.RightToLeft:
                StatusGrid.ColumnDefinitions[0].Width = GridLength.Auto;
                StatusGrid.ColumnDefinitions[1].Width = GridLength.Star;
                StatusLabel.SetValue(Grid.ColumnProperty, 1);
                StatusDot.SetValue(Grid.ColumnProperty, 0);
                break;
            case FlowDirection.LeftToRight:
            default:
                break;
        }
    }

    private static void OnTitlePropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((DotStatusWithTitle)bindable).StatusLabel.Text = (string)newValue;
    }

    private static void OnStatusColorPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((DotStatusWithTitle)bindable).StatusDot.Fill = (Color)newValue;
    }
}