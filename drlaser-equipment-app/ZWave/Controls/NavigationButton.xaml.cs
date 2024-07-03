using ZWave.Helpers;
using ZWave.Shared.Enums;

namespace ZWave.Controls;

public partial class NavigationButton : ContentView
{
    static readonly string TAP_STATE = "Tap";
    static readonly string POINTER_OVER_STATE = "PointerOver";

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationButton), string.Empty, default, propertyChanged: OnTitleChanged);

    public TabPage TabPage { get; set; }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    private string _normalIcon;
    public string NormalIcon
    {
        get => _normalIcon;
        set
        {
            _normalIcon = value;

            if (IconImage.Source is null)
            {
                IconImage.Source = value;
            }
        }
    }
    public string SelectedIcon { get; set; }
    public Color SelectedBackgroundColor { get; set; } = ResourceHelper.GetColor("BlueBrand5");

    private bool _isSelected;
    public bool Selected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value)
            {
                return;
            }

            _isSelected = value;

            if (_isSelected)
            {
                IconImage.Source = SelectedIcon;
                TitleLabel.TextColor = ResourceHelper.GetColor("White");
                StackLayout.BackgroundColor = SelectedBackgroundColor;
                return;
            }

            IconImage.Source = NormalIcon;
            TitleLabel.TextColor = ResourceHelper.GetColor("Black");
            StackLayout.BackgroundColor = ResourceHelper.GetColor("White");
        }
    }

    public bool IsAtBarEnd { get; set; } = false;

    public NavigationButton()
    {
        InitializeComponent();
    }

    public event EventHandler OnTappedHandler;
    private void OnTapped(object sender, TappedEventArgs e)
    {
        Selected = true;

        OnTappedHandler?.Invoke(this, e);

        GoToState(TAP_STATE);
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            GoToState(POINTER_OVER_STATE);
        });
    }

    void GoToState(string state)
    {
        VisualStateManager.GoToState(StackLayout, state);
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var navigationButton = (NavigationButton)bindable;
        navigationButton.TitleLabel.Text = (string)newValue;
    }
}
