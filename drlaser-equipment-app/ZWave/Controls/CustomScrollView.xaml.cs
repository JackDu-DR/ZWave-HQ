using System.Timers;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class CustomScrollView : ContentView
{
    private static readonly double DEFAULT_SCROLL_THUMB_THICKNESS = (double)ResourceHelper.GetDimension("CustomScrollViewDefaultThumbSize");
    private Grid _scrollViewLayout => (Grid)GetTemplateChild("ScrollViewLayout");
    private CustomScrollBar _verticalScrollBar => (CustomScrollBar)GetTemplateChild("VerticalScrollBar");
    private CustomScrollBar _horizontalScrollBar => (CustomScrollBar)GetTemplateChild("HorizontalScrollBar");
    private Point _scrolledPoint = new Point(0, 0);
    private Point _scrolledEndPoint = new Point(0, 0);
    private System.Timers.Timer _scrollTimer;

    public static readonly BindableProperty ViewportWidthProperty = BindableProperty.Create(nameof(ViewportWidth), typeof(double), typeof(CustomScrollView), default);
    public static readonly BindableProperty ViewportHeightProperty = BindableProperty.Create(nameof(ViewportHeight), typeof(double), typeof(CustomScrollView), default);
    public static readonly BindableProperty ViewportMaxWidthProperty = BindableProperty.Create(nameof(ViewportMaxWidth), typeof(double), typeof(CustomScrollView), default, propertyChanged: OnViewportMaxWidthChanged);
    public static readonly BindableProperty ViewportMaxHeightProperty = BindableProperty.Create(nameof(ViewportMaxHeight), typeof(double), typeof(CustomScrollView), default, propertyChanged: OnViewportMaxHeightChanged);
    public static readonly BindableProperty IsScrollableProperty = BindableProperty.Create(nameof(IsScrollable), typeof(bool), typeof(CustomScrollView), null, propertyChanged: OnScrollablePropertyChanged);
    public static readonly BindableProperty ScrollThumbThicknessProperty = BindableProperty.Create(nameof(ScrollThumbThickness), typeof(double), typeof(CustomScrollView), default, propertyChanged: OnScrollThumbThicknessPropertyChanged);
    public static readonly BindableProperty ScrollXProperty = BindableProperty.Create(nameof(ScrollX), typeof(double), typeof(CustomScrollView), null, propertyChanged: OnScrollXPropertyChanged);
    public static readonly BindableProperty ScrollYProperty = BindableProperty.Create(nameof(ScrollY), typeof(double), typeof(CustomScrollView), null, propertyChanged: OnScrollYPropertyChanged);
    
    public static readonly BindableProperty RatioProperty = BindableProperty.Create(nameof(Ratio), typeof(int), typeof(CustomScrollView), null, propertyChanged: OnRatioPropertyChanged);

    /// <summary>
    /// Used to calculate the viewport width, in-dependant and not affect to ChildView width
    /// </summary>
    public double ViewportWidth
    {
        get => (double)GetValue(ViewportWidthProperty);
        set => SetValue(ViewportWidthProperty, value);
    }

    /// <summary>
    /// Used to calculate the viewport height, in-dependant and not affect to ChildView height
    /// </summary>
    public double ViewportHeight
    {
        get => (double)GetValue(ViewportHeightProperty);
        set => SetValue(ViewportHeightProperty, value);
    }

    /// <summary>
    /// Used with ViewportWidth to calculate horizontal scrolling area, normally ChildView max width
    /// </summary>
    public double ViewportMaxWidth
    {
        get => (double)GetValue(ViewportMaxWidthProperty);
        set => SetValue(ViewportMaxWidthProperty, value);
    }

    /// <summary>
    /// Used with ViewportHeight to calculate vertical scrolling area, normally ChildView max height
    /// </summary>
    public double ViewportMaxHeight
    {
        get => (double)GetValue(ViewportMaxHeightProperty);
        set => SetValue(ViewportMaxHeightProperty, value);
    }

    /// <summary>
    /// Scroll thumb width/height of non-scrolling orientation
    /// </summary>
    public double ScrollThumbThickness
    {
        get => (double)GetValue(ScrollThumbThicknessProperty);
        set => SetValue(ScrollThumbThicknessProperty, value);
    }

    public bool IsScrollable
    {
        get { return (bool)GetValue(IsScrollableProperty); }
        set { SetValue(IsScrollableProperty, value); }
    }

    public double ScrollX
    {
        get { return (double)GetValue(ScrollXProperty); }
        set { SetValue(ScrollXProperty, value); }
    }

    public double ScrollY
    {
        get { return (double)GetValue(ScrollYProperty); }
        set { SetValue(ScrollYProperty, value); }
    }

    public int Ratio
    {
        get { return (int)GetValue(RatioProperty); }
        set { SetValue(RatioProperty, value); }
    }

    public event EventHandler<ScrolledEventArgs> Scrolled;
    public event EventHandler<Point> EndScrolled;

    public CustomScrollView()
	{
		InitializeComponent();

        _scrollTimer = new System.Timers.Timer(200);
        _scrollTimer.AutoReset = false;
        _scrollTimer.Elapsed += OnScrollTimerElapsed;

        Loaded += OnLoaded;
	}

    private void OnLoaded(object sender, EventArgs e)
    {
        ScrollThumbThickness = ScrollThumbThickness != 0 ? ScrollThumbThickness : DEFAULT_SCROLL_THUMB_THICKNESS;

        _scrollViewLayout.RowDefinitions = new RowDefinitionCollection([
            new RowDefinition { Height = GridLength.Star },
            new RowDefinition { Height = ScrollThumbThickness },
        ]);

        _scrollViewLayout.ColumnDefinitions = new ColumnDefinitionCollection([
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = ScrollThumbThickness },
        ]);

        var contentPresenter = (ContentPresenter)GetTemplateChild("ScrollViewContent");
        _scrollViewLayout.SetRow(contentPresenter, 0);
        _scrollViewLayout.SetColumn(contentPresenter, 0);

        _verticalScrollBar.HeightRequest = ViewportHeight;
        _verticalScrollBar.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
        _scrollViewLayout.SetRow(_verticalScrollBar, 0);
        _scrollViewLayout.SetColumn(_verticalScrollBar, 1);

        _horizontalScrollBar.WidthRequest = ViewportWidth;
        _horizontalScrollBar.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
        _scrollViewLayout.SetRow(_horizontalScrollBar, 1);
        _scrollViewLayout.SetColumn(_horizontalScrollBar, 0);
    }

    private static void OnViewportMaxWidthChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customScrollView = (CustomScrollView)bindable;
        customScrollView.UpdateHorizontalScrollBarMaximumValue();
    }

    private static void OnViewportMaxHeightChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customScrollView = (CustomScrollView)bindable;
        customScrollView.UpdateVerticalScrollBarMaximumValue();
    }

    private static void OnScrollablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var scrollView = (CustomScrollView)bindable;
        scrollView._verticalScrollBar.IsEnabled = (bool)newValue;
        scrollView._horizontalScrollBar.IsEnabled = (bool)newValue;
    }

    private static void OnScrollThumbThicknessPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var scrollView = (CustomScrollView)bindable;
        if (scrollView._scrollViewLayout.RowDefinitions.Count() > 1 && scrollView._scrollViewLayout.ColumnDefinitions.Count() > 1)
        {
            scrollView._scrollViewLayout.RowDefinitions[1].Height = (double)newValue;
            scrollView._scrollViewLayout.ColumnDefinitions[1].Width = (double)newValue;
        }
    }

    private void VerticalScrollBar_Scrolled(object sender, ScrolledEventArgs e)
    {
        _scrolledPoint.Y = e.ScrollY;
        Scrolled?.Invoke(this, new ScrolledEventArgs(_scrolledPoint.X, _scrolledPoint.Y));

        _scrollTimer.Stop();
        _scrollTimer.Start();

        _scrolledEndPoint.Y = e.ScrollY;
    }

    private void HorizontalScrollBar_Scrolled(object sender, ScrolledEventArgs e)
    {
        _scrolledPoint.X = e.ScrollX;
        Scrolled?.Invoke(this, new ScrolledEventArgs(_scrolledPoint.X, _scrolledPoint.Y));

        _scrollTimer.Stop();
        _scrollTimer.Start();

        _scrolledEndPoint.X = e.ScrollX;
    }

    private void OnScrollTimerElapsed(object sender, ElapsedEventArgs e)
    {
        EndScrolled?.Invoke(sender, _scrolledPoint);
    }

    private static void OnScrollXPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customScrollView = (CustomScrollView)bindable;
        if (customScrollView._scrolledPoint.X != (double)newValue)
        {
            customScrollView._horizontalScrollBar.ScrollTo((double)newValue);
            customScrollView._scrolledPoint.X = (double)newValue;
            customScrollView._scrolledEndPoint.X = (double)newValue;
        }
    }

    private static void OnScrollYPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customScrollView = (CustomScrollView)bindable;
        if (customScrollView._scrolledPoint.Y != (double)newValue)
        {
            customScrollView._verticalScrollBar.ScrollTo((double)newValue);
            customScrollView._scrolledPoint.Y = (double)newValue;
            customScrollView._scrolledEndPoint.Y = (double)newValue;
        }
    }

    private static void OnRatioPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customScrollView = (CustomScrollView)bindable;
        customScrollView.UpdateHorizontalScrollBarMaximumValue();
        customScrollView.UpdateVerticalScrollBarMaximumValue();
    }

    private void UpdateHorizontalScrollBarMaximumValue()
    {
        _horizontalScrollBar.MaximumScrollValue =
            ViewportMaxWidth > 0 && ViewportMaxWidth / Ratio > ViewportWidth ? ViewportMaxWidth / Ratio : ViewportWidth;
    }

    private void UpdateVerticalScrollBarMaximumValue()
    {
        _verticalScrollBar.MaximumScrollValue =
            ViewportMaxHeight > 0 && ViewportMaxHeight / Ratio > ViewportHeight ? ViewportMaxHeight / Ratio : ViewportHeight;
    }
}