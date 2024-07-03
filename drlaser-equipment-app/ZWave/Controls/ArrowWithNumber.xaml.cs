namespace ZWave.Controls;

public partial class ArrowWithNumber : ContentView
{
    public static readonly BindableProperty ArrowNumberTextProperty = BindableProperty.Create(nameof(ArrowNumberText), typeof(string), typeof(ArrowWithNumber), "10", propertyChanged: OnArrowNumberTextPropertyChange);
    public static readonly BindableProperty ArrowRotationProperty = BindableProperty.Create(nameof(ArrowRotation), typeof(int), typeof(ArrowWithNumber), 0);
    public static readonly BindableProperty ChangeArrowPositionProperty = BindableProperty.Create(nameof(ChangeArrowPosition), typeof(int), typeof(ArrowWithNumber), 0);

    public string ArrowNumberText
    {
        get => (string)GetValue(ArrowNumberTextProperty);
        set => SetValue(ArrowNumberTextProperty, value);
    }

    public int ArrowRotation
    {
        get => (int)GetValue(ArrowRotationProperty);
        set => SetValue(ArrowRotationProperty, value);
    }
    public int ChangeArrowPosition
    {
        get => (int)GetValue(ChangeArrowPositionProperty);
        set => SetValue(ChangeArrowPositionProperty, value);
    }

    public ArrowWithNumber()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        ColumnDefinition column1 = new();
        ColumnDefinition column2 = new();

        ArrowStyleGrid.ColumnDefinitions.Clear();

        if (ChangeArrowPosition == 0)
        {
            column1.Width = new GridLength(1, GridUnitType.Star);
            column2.Width = new GridLength(0.05, GridUnitType.Star);
            ArrowStyleGrid.ColumnDefinitions.Add(column1);
            ArrowStyleGrid.ColumnDefinitions.Add(column2);
            Grid.SetColumn(ArrowNumber, 0);
            Grid.SetColumn(ArrowIcon, 1);
        }
        else
        {
            column1.Width = new GridLength(0.05, GridUnitType.Star);
            column2.Width = new GridLength(1, GridUnitType.Star);
            ArrowStyleGrid.ColumnDefinitions.Add(column1);
            ArrowStyleGrid.ColumnDefinitions.Add(column2);
            Grid.SetColumn(ArrowNumber, 1);
            Grid.SetColumn(ArrowIcon, 0);
        }

        ArrowNumber.Text = ArrowNumberText;
        if (ArrowRotation != 0)
        {
            ArrowStyleGrid.Rotation = ArrowRotation;
            ArrowNumber.Rotation = 360 - ArrowRotation;
        }
    }
    private static void OnArrowNumberTextPropertyChange(BindableObject bindable, object _, object newValue)
    {
        ((ArrowWithNumber)bindable).ArrowNumber.Text = (string)newValue;
    }
}