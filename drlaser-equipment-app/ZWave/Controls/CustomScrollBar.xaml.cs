namespace ZWave.Controls;

public enum CustomScrollBarOrientation
{
    Vertical,
    Horizontal,
}

public partial class CustomScrollBar : Grid
{
    private static readonly int ROUND_TO_DECIMAL = 10;

    private double _maximumScrollOffset;
    private double _maximumScrollValueRatio;
    private double _thumbTransitionBeforeScroll;
    private Point? _startPoint;
    private bool _scrolling;

    public readonly BindableProperty MaximumScrollValueProperty = BindableProperty.Create(nameof(MaximumScrollValue), typeof(double), typeof(CustomScrollBar), default, propertyChanged: OnMaximumScrollValueChanged);

    public CustomScrollBarOrientation CustomScrollBarOrientation { get; set; }

    public double MaximumScrollValue
    {
        get => (double)GetValue(MaximumScrollValueProperty);
        set => SetValue(MaximumScrollValueProperty, value);
    }

    public event EventHandler<ScrolledEventArgs> Scrolled;
    public CustomScrollBar()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    /// <summary>
    /// This function used to update scrollbar postion only based on current view state without furthur action
    /// </summary>
    /// <param name="value"></param>
    public void ScrollTo(double value)
    {
        _thumbTransitionBeforeScroll = 0;
        var newScrollPosition = value / _maximumScrollValueRatio;
        switch (CustomScrollBarOrientation)
        {
            case CustomScrollBarOrientation.Horizontal:
                ScrollThumbHorizontalToPoint(new Point(0, 0), new Point(newScrollPosition, 0), value);
                break;
            case CustomScrollBarOrientation.Vertical:
                ScrollThumbVerticalToPoint(new Point(0, 0), new Point(0, newScrollPosition), value);
                break;
            default:
                break;
        }
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        SetupScrollView(this, MaximumScrollValue);
        switch (CustomScrollBarOrientation)
        {
            case CustomScrollBarOrientation.Horizontal:
                Thumb.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
                Thumb.HorizontalOptions = LayoutOptions.Start;
                break;
            case CustomScrollBarOrientation.Vertical:
                Thumb.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
                Thumb.VerticalOptions = LayoutOptions.Start;
                break;
            default:
                break;
        }
    }

    private void GraphicView_Touch(object sender, SkiaSharp.Views.Maui.SKTouchEventArgs e)
    {
        var point = new Point(e.Location.X, e.Location.Y);
        switch (e.ActionType)
        {
            case SkiaSharp.Views.Maui.SKTouchAction.Pressed:
                OnPressed(point);
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Moved:
                OnMoved(point);
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Released:
                OnReleased();
                e.Handled = false;
                break;
        }
    }

    private void OnPressed(Point point)
    {
        switch (CustomScrollBarOrientation)
        {
            case CustomScrollBarOrientation.Horizontal:
                _thumbTransitionBeforeScroll = Thumb.TranslationX;
                if (Thumb.TranslationX <= point.X && point.X <= Thumb.TranslationX + Thumb.WidthRequest)
                {
                    _scrolling = true;
                    _startPoint = point;
                }
                else
                {
                    var jumpRange = point.X > Thumb.TranslationX ? WidthRequest : -WidthRequest;
                    var nextXPoint = ((jumpRange + (Thumb.TranslationX * _maximumScrollValueRatio)) / _maximumScrollValueRatio) + Thumb.TranslationX - _thumbTransitionBeforeScroll;
                    ScrollThumbHorizontalToPoint(new Point(Thumb.TranslationX, 0), new Point(nextXPoint, 0));
                }
                break;
            case CustomScrollBarOrientation.Vertical:
            default:
                _thumbTransitionBeforeScroll = Thumb.TranslationY;
                if (Thumb.TranslationY <= point.Y && point.Y <= Thumb.TranslationY + Thumb.HeightRequest)
                {
                    _scrolling = true;
                    _startPoint = point;
                }
                else
                {
                    var jumpRange = point.Y > Thumb.TranslationY ? HeightRequest : -HeightRequest;
                    var nextYPoint = ((jumpRange + (Thumb.TranslationY * _maximumScrollValueRatio)) / _maximumScrollValueRatio) + Thumb.TranslationY - _thumbTransitionBeforeScroll;
                    ScrollThumbVerticalToPoint(new Point(0, Thumb.TranslationY), new Point(0, nextYPoint));
                }
                break;
        }
    }

    private void OnMoved(Point point)
    {
        if (_scrolling)
        {
            ScrollThumbToPoint(_startPoint, point);
        }
    }

    private void OnReleased()
    {
        _scrolling = false;
        _startPoint = null;
        _thumbTransitionBeforeScroll = 0;
    }

    private void ScrollThumbToPoint(Point? startPoint, Point nextPoint)
    {
        if (!startPoint.HasValue)
        {
            return;
        }

        switch (CustomScrollBarOrientation)
        {
            case CustomScrollBarOrientation.Horizontal:
                ScrollThumbHorizontalToPoint(startPoint.Value, nextPoint);
                break;
            case CustomScrollBarOrientation.Vertical:
            default:
                ScrollThumbVerticalToPoint(startPoint.Value, nextPoint);
                break;

        }
    }

    private void ScrollThumbHorizontalToPoint(Point? startPoint, Point nextPoint, double? currentScrollXValue = null)
    {
        var newOffset = _thumbTransitionBeforeScroll + nextPoint.X - startPoint.Value.X;
        if (Thumb.TranslationX > 0 && newOffset <= 0)
        {
            newOffset = 0;
        }
        else if ((Thumb.TranslationX < _maximumScrollOffset && newOffset >= _maximumScrollOffset) || (newOffset + Thumb.WidthRequest > WidthRequest))
        {
            newOffset = _maximumScrollOffset;
        }

        if (newOffset >= 0 && newOffset <= _maximumScrollOffset && newOffset != Thumb.TranslationX)
        {
            Thumb.TranslationX = newOffset;
            var newScrollXValue = newOffset * _maximumScrollValueRatio;
            if (currentScrollXValue == null || Math.Round((double)currentScrollXValue, ROUND_TO_DECIMAL) != Math.Round(newScrollXValue, ROUND_TO_DECIMAL))
            {
                Scrolled?.Invoke(this, new ScrolledEventArgs(Math.Round(newScrollXValue, ROUND_TO_DECIMAL), 0));
            }
        }
    }

    private void ScrollThumbVerticalToPoint(Point startPoint, Point nextPoint, double? currentScrollYValue = null)
    {
        var newOffset = _thumbTransitionBeforeScroll + nextPoint.Y - startPoint.Y;
        if (Thumb.TranslationY > 0 && newOffset <= 0)
        {
            newOffset = 0;
        }
        else if ((Thumb.TranslationY < _maximumScrollOffset && newOffset >= _maximumScrollOffset) || (newOffset + Thumb.HeightRequest > HeightRequest))
        {
            newOffset = _maximumScrollOffset;
        }

        if (newOffset >= 0 && newOffset <= _maximumScrollOffset && newOffset != Thumb.TranslationY)
        {
            Thumb.TranslationY = newOffset;
            var newScrollYValue = newOffset * _maximumScrollValueRatio;
            if (currentScrollYValue == null || Math.Round((double)currentScrollYValue, ROUND_TO_DECIMAL) != Math.Round(newScrollYValue, ROUND_TO_DECIMAL))
            {
                Scrolled?.Invoke(this, new ScrolledEventArgs(0, Math.Round(newScrollYValue, ROUND_TO_DECIMAL)));
            }
        }
    }

    private static void OnMaximumScrollValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        SetupScrollView((CustomScrollBar)bindable, (double)newValue);
    }

    private static void SetupScrollView(CustomScrollBar scrollBar, double newMaximumScrollValue)
    {
            switch (scrollBar.CustomScrollBarOrientation)
            {
                case CustomScrollBarOrientation.Horizontal:
                    newMaximumScrollValue = newMaximumScrollValue > 0 ? newMaximumScrollValue : scrollBar.WidthRequest;
                    if (scrollBar.IsVisible || newMaximumScrollValue > scrollBar.WidthRequest)
                    {
                        var currentScrollXValue = scrollBar.Thumb.TranslationX * scrollBar._maximumScrollValueRatio;
                        scrollBar.IsVisible = newMaximumScrollValue > scrollBar.WidthRequest;
                        scrollBar.Thumb.WidthRequest = scrollBar.WidthRequest * scrollBar.WidthRequest / newMaximumScrollValue;
                        scrollBar._maximumScrollOffset = scrollBar.WidthRequest - scrollBar.Thumb.WidthRequest;
                        scrollBar._maximumScrollValueRatio = newMaximumScrollValue / scrollBar.WidthRequest;
                        scrollBar.ScrollThumbHorizontalToPoint(new Point(0, 0), new Point(currentScrollXValue / scrollBar._maximumScrollValueRatio, 0), currentScrollXValue);
                    }
                    break;
                case CustomScrollBarOrientation.Vertical:
                    newMaximumScrollValue = newMaximumScrollValue > 0 ? newMaximumScrollValue : scrollBar.HeightRequest;
                    if (scrollBar.IsVisible || newMaximumScrollValue > scrollBar.HeightRequest)
                    {
                        var currentScrollYValue = scrollBar.Thumb.TranslationY * scrollBar._maximumScrollValueRatio;
                        scrollBar.IsVisible = newMaximumScrollValue > scrollBar.HeightRequest;
                        scrollBar.Thumb.HeightRequest = scrollBar.HeightRequest * scrollBar.HeightRequest / newMaximumScrollValue;
                        scrollBar._maximumScrollOffset = scrollBar.HeightRequest - scrollBar.Thumb.HeightRequest;
                        scrollBar._maximumScrollValueRatio = newMaximumScrollValue / scrollBar.HeightRequest;
                        scrollBar.ScrollThumbVerticalToPoint(new Point(0, 0), new Point(0, currentScrollYValue / scrollBar._maximumScrollValueRatio), currentScrollYValue);
                    }
                    break;
                default:
                    break;
            }
    }
}