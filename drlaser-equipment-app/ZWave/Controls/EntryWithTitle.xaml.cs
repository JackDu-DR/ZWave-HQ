using ZWave.Helpers;

namespace ZWave.Controls;

public partial class EntryWithTitle : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(EntryWithTitle), string.Empty, propertyChanged: OnTitlePropertyChange);
    public static readonly BindableProperty LeftEntryEnabledProperty = BindableProperty.Create(nameof(LeftEntryEnabled), typeof(bool), typeof(EntryWithTitle), true, propertyChanged: LeftEntryEnabledChanged);
    public static readonly BindableProperty RightEntryEnabledProperty = BindableProperty.Create(nameof(RightEntryEnabled), typeof(bool), typeof(EntryWithTitle), true, propertyChanged: RightEntryEnabledChanged);
    public static readonly BindableProperty LeftTextProperty = BindableProperty.Create(nameof(LeftText), typeof(string), typeof(EntryWithTitle), string.Empty, BindingMode.TwoWay, propertyChanged: OnLeftEntryChanged);
    public static readonly BindableProperty LeftTextErrorMessageProperty = BindableProperty.Create(nameof(LeftTextErrorMessage), typeof(string), typeof(EntryWithTitle), string.Empty, propertyChanged: OnLeftEntryErrorMessageChanged);
    public static readonly BindableProperty RightTextProperty = BindableProperty.Create(nameof(RightText), typeof(string), typeof(EntryWithTitle), string.Empty, BindingMode.TwoWay, propertyChanged: OnRightEntryChanged);
    public static readonly BindableProperty RightTextErrorMessageProperty = BindableProperty.Create(nameof(RightTextErrorMessage), typeof(string), typeof(EntryWithTitle), string.Empty, propertyChanged: OnRightTextErrorMessageChanged);
    public static readonly BindableProperty UnitProperty = BindableProperty.Create(nameof(Unit), typeof(string), typeof(EntryWithTitle), string.Empty, propertyChanged: OnUnitPropertyChange);
    public static readonly BindableProperty UnitMarginProperty = BindableProperty.Create(nameof(UnitMargin), typeof(Thickness), typeof(EntryWithTitle), default, propertyChanged: OnUnitMarginPropertyChange);

    private static readonly Color DEFAULT_ENTRY_BORDER_COLOR = ResourceHelper.GetColor("EntryBorder");
    private static readonly Color ERROR_ENTRY_BORDER_COLOR = ResourceHelper.GetColor("Red6");
    private static readonly double UNIT_WIDTH = (double)ResourceHelper.GetDimension("EntryWithTitleControlsUnitWidth");
    private static readonly double SMALL_SPACING = (double)ResourceHelper.GetDimension("SmallSpacing");

    public StackOrientation Orientation { get; set; } = StackOrientation.Horizontal;
    public string PlaceHolder { get; set; }
    public double TitleWidthRequest { get; set; }
    public double EntryContainerWidthRequest { get; set; }
    public bool IsSingleEntry { get; set; }
    public Keyboard Keyboard { get; set; } = Keyboard.Default;
    public FontAttributes FontAttributes { get; set; } = FontAttributes.None;
    // Remarks:
    //     Set this property to True will ignore the value of TitleWidthRequest.
    public bool IsTitleWidthAutoResize { get; set; }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool LeftEntryEnabled
    {
        get => (bool)GetValue(LeftEntryEnabledProperty);
        set => SetValue(LeftEntryEnabledProperty, value);
    }

    public bool RightEntryEnabled
    {
        get => (bool)GetValue(RightEntryEnabledProperty);
        set => SetValue(RightEntryEnabledProperty, value);
    }

    public string LeftText
    {
        get => (string)GetValue(LeftTextProperty);
        set => SetValue(LeftTextProperty, value);
    }

    public string LeftTextErrorMessage
    {
        get => (string)GetValue(LeftTextErrorMessageProperty);
        set => SetValue(LeftTextErrorMessageProperty, value);
    }

    public string RightText
    {
        get => (string)GetValue(RightTextProperty);
        set => SetValue(RightTextProperty, value);
    }

    public string RightTextErrorMessage
    {
        get => (string)GetValue(RightTextErrorMessageProperty);
        set => SetValue(RightTextErrorMessageProperty, value);
    }

    public string Unit
    {
        get => (string)GetValue(UnitProperty);
        set => SetValue(UnitProperty, value);
    }

    public Thickness UnitMargin
    {
        get => (Thickness)GetValue(UnitMarginProperty);
        set => SetValue(UnitMarginProperty, value);
    }

    public EntryWithTitle()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        SetupContainerDirection();
        SetupLeftEntryStatus(this, LeftTextErrorMessage);
        SetupRightEntryStatus(this, RightTextErrorMessage);
        SetupLabelAndUnit(this);
        TitleLabel.FontAttributes = FontAttributes;
        TitleLabel.WidthRequest = -1;
        Container.ColumnDefinitions[0].Width = IsTitleWidthAutoResize ? GridLength.Star : TitleWidthRequest;

        Grid.WidthRequest = EntryContainerWidthRequest + (string.IsNullOrEmpty(Unit) ? 0 : UNIT_WIDTH);
        Grid.ColumnDefinitions[1].Width = IsSingleEntry ? 0 : GridLength.Star;

        RightEntryLayout.IsVisible = !IsSingleEntry;

        LeftEntry.Text = LeftText;
        RightEntry.Text = RightText;

        LeftEntry.Keyboard = Keyboard;
        RightEntry.Keyboard = Keyboard;

        UnitLabel.Text = Unit;
        UnitLabel.Margin = UnitMargin;

        if (string.IsNullOrEmpty(Unit))
        {
            Grid.ColumnDefinitions[2].Width = 0;
            UnitLabel.IsVisible = false;
        }
    }

    private void SetupContainerDirection()
    {
        switch (Orientation)
        {
            case StackOrientation.Vertical:
                Container.RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                };
                Container.RowSpacing = 8;
                TitleLabel.SetValue(Grid.ColumnProperty, 0);
                TitleLabel.SetValue(Grid.RowProperty, 0);
                TitleLabel.HorizontalOptions = LayoutOptions.Start;
                Grid.SetValue(Grid.ColumnProperty, 0);
                Grid.SetValue(Grid.RowProperty, 1);
                Grid.HorizontalOptions = LayoutOptions.Start;
                break;
            case StackOrientation.Horizontal:
            default:
                break;
        }
    }

    private void OnLeftTextChange(object sender, TextChangedEventArgs e)
    {
        LeftText = ((Entry)sender).Text;
    }

    private void OnRightTextChange(object sender, TextChangedEventArgs e)
    {
        RightText = ((Entry)sender).Text;
    }

    private static void OnTitlePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((EntryWithTitle)bindable).TitleLabel.Text = (string)newValue;
    }

    private static void OnLeftEntryChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((EntryWithTitle)bindable).LeftEntry.Text = (string)newValue;
    }

    private static void OnRightEntryChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((EntryWithTitle)bindable).RightEntry.Text = (string)newValue;
    }

    private static void LeftEntryEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var leftEntry = ((EntryWithTitle)bindable).LeftEntry;
        leftEntry.IsEnabled = (bool)newValue;
        leftEntry.BackgroundColor = (bool)newValue ? ResourceHelper.GetColor("White") : ResourceHelper.GetColor("GreyNeutral3");
    }

    private static void RightEntryEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var rightEntry = ((EntryWithTitle)bindable).RightEntry;
        rightEntry.IsEnabled = (bool)newValue;
        rightEntry.BackgroundColor = (bool)newValue ? ResourceHelper.GetColor("White") : ResourceHelper.GetColor("GreyNeutral3");
    }

    private static void OnUnitPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((EntryWithTitle)bindable).UnitLabel.Text = (string)newValue;
    }

    private static void OnUnitMarginPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((EntryWithTitle)bindable).UnitLabel.Margin = (Thickness)newValue;
    }

    private static void OnLeftEntryErrorMessageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        SetupLeftEntryStatus((EntryWithTitle)bindable, (string)newValue);
    }

    private static void OnRightTextErrorMessageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        SetupRightEntryStatus((EntryWithTitle)bindable, (string)newValue);
    }

    private static void SetupLeftEntryStatus(EntryWithTitle entryWithTitle, string leftTextErrorMessage)
    {
        var isLeftTextValid = string.IsNullOrEmpty(leftTextErrorMessage);
        SetupLabelAndUnit(entryWithTitle, isLeftTextValid);
        entryWithTitle.LeftEntryGrid.RowDefinitions[1].Height = isLeftTextValid ? 0 : GridLength.Star;
        entryWithTitle.LeftEntryLayout.Stroke = isLeftTextValid ? DEFAULT_ENTRY_BORDER_COLOR : ERROR_ENTRY_BORDER_COLOR;
        entryWithTitle.InvalidMessageLeftLabel.Text = leftTextErrorMessage;
    }

    private static void SetupRightEntryStatus(EntryWithTitle entryWithTitle, string rightTextErrorMessage)
    {
        var isRightTextValid = string.IsNullOrEmpty(rightTextErrorMessage);
        SetupLabelAndUnit(entryWithTitle, isRightTextValid);
        entryWithTitle.RightEntryGrid.RowDefinitions[1].Height = isRightTextValid ? 0 : GridLength.Star;
        entryWithTitle.RightEntryLayout.Stroke = isRightTextValid ? DEFAULT_ENTRY_BORDER_COLOR : ERROR_ENTRY_BORDER_COLOR;
        entryWithTitle.InvalidMessageRightLabel.Text = rightTextErrorMessage;
    }

    private static void SetupLabelAndUnit(EntryWithTitle entryWithTitle, bool isValidateFailed = false)
    {
        var isBothEntryTextValid = string.IsNullOrEmpty(entryWithTitle.LeftTextErrorMessage) &&
            string.IsNullOrEmpty(entryWithTitle.RightTextErrorMessage);
        if (isValidateFailed || !isBothEntryTextValid)
        {
            if (entryWithTitle.HeightRequest != -1)
            {
                entryWithTitle.HeightRequest = -1;
            }
            entryWithTitle.TitleLabel.VerticalOptions = LayoutOptions.Start;
            entryWithTitle.TitleLabel.Margin = new Thickness(0, SMALL_SPACING, 0, 0);
            entryWithTitle.UnitLabel.VerticalOptions = LayoutOptions.Start;
            entryWithTitle.UnitLabel.Margin = new Thickness(SMALL_SPACING, SMALL_SPACING, 0, 0);
        }
        else
        {
            entryWithTitle.TitleLabel.VerticalOptions = LayoutOptions.Center;
            entryWithTitle.TitleLabel.Margin = new Thickness(0);
            entryWithTitle.UnitLabel.VerticalOptions = LayoutOptions.Center;
            entryWithTitle.UnitLabel.Margin = new Thickness(SMALL_SPACING, 0, 0, 0);
        }
    }
}