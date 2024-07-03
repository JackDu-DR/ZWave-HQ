using Microsoft.Maui.Controls.Shapes;
using ZWave.Helpers;

namespace ZWave.Controls;

public partial class CustomTabItem : ContentView
{
    public static readonly BindableProperty TabTypeProperty = BindableProperty.Create(nameof(TabType), typeof(TabType), typeof(CustomTabView), default);
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomTabView), string.Empty, propertyChanged: OnTitlePropertyChanged);

    public TabType TabType
    {
        get => (TabType)GetValue(TabTypeProperty);
        set => SetValue(TabTypeProperty, value);
    }

    /// <summary>
    ///Get or Set Tab Item Title
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    private bool _indicatorIsVisible;
    public bool IndicatorIsVisible
    {
        get => _indicatorIsVisible;
        set
        {
            _indicatorIsVisible = value;
            SetVisualState(State);
        }
    }

    private bool _borderVisible;
    public bool BorderVisible
    {
        get => _borderVisible;
        set
        {
            _borderVisible = value;

            if (!_borderVisible)
            {
                Border.Stroke = Colors.Transparent;
            }
        }
    }

    public Color TabColor { get; set; }

    private TabItemState _state = TabItemState.Normal;
    public TabItemState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                //set style for whole view according to the state
                SetVisualState(value);
            }
        }
    }

    private CornerRadius _cornerRadius { get; set; }
    public CornerRadius CornerRadius
    {
        get => _cornerRadius; set
        {
            _cornerRadius = value;
            SetCornerRadius(value);
        }
    }

    private void SetVisualState(TabItemState value)
    {
        switch (value)
        {
            case TabItemState.Normal:
                IndicatorView.IsVisible = false;
                SetCornerRadius(CornerRadius);
                Border.BackgroundColor = TabColor;
                TitleLabel.TextColor = ResourceHelper.GetColor("GreyNeutral11");
                break;
            case TabItemState.Selected:
                TitleLabel.TextColor = Colors.Blue;
                if (_indicatorIsVisible)
                {
                    IndicatorView.IsVisible = true;
                }
                SetCornerRadius(new CornerRadius(8, 8, 0, 0));
                Border.BackgroundColor = Colors.White;
                TitleLabel.TextColor = ResourceHelper.GetColor("GreyNeutral14");
                break;
            case TabItemState.PointerOver:
                TitleLabel.TextColor = Colors.Gray;
                break;
            default: break;

        }
    }

    public CustomTabItem()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        SetVisualState(_state);
        Loaded -= OnLoaded;
    }

    public event EventHandler OnTappedHandler;
    private void OnTapped(object sender, TappedEventArgs e)
    {
        //raise event on tapped, so that the view who use this tabitem know and handle loading page content
        OnTappedHandler?.Invoke(this, e);
    }

    private void SetCornerRadius(CornerRadius cornerRadius)
    {
        Border.StrokeShape = new RoundRectangle() { CornerRadius = cornerRadius };
    }

    public enum TabItemState
    {
        Normal = 0,
        Selected = 1,
        PointerOver = 2
    }

    private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabItem = (CustomTabItem)bindable;
        tabItem.TitleLabel.Text = (string)newValue;
    }

    //TODO Dispose
}