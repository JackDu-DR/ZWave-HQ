using ZWave.Helpers;

namespace ZWave.Controls;

public partial class StateButton
{
    public static readonly BindableProperty SelectStateProperty = BindableProperty.Create(nameof(SelectState), typeof(SelectedState), typeof(StateButton), SelectedState.Left, BindingMode.TwoWay, propertyChanged: OnSelectStatePropertyChange);
    public static readonly BindableProperty LeftTextProperty = BindableProperty.Create(nameof(LeftText), typeof(string), typeof(StateButton), string.Empty, BindingMode.OneWay, propertyChanged: OnLeftTextPropertyChange);
    public static readonly BindableProperty RightTextProperty = BindableProperty.Create(nameof(RightText), typeof(string), typeof(StateButton), string.Empty, BindingMode.OneWay, propertyChanged: OnRightTextPropertyChange);

    /// <summary>
    /// Gets or sets a value indicating Left or Right is selected.
    /// This propery is bindable
    /// </summary>
    public SelectedState SelectState
    {
        get => (SelectedState)GetValue(SelectStateProperty);
        set => SetValue(SelectStateProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating left button title.
    /// </summary>
    public string LeftText
    {
        get => (string)GetValue(LeftTextProperty);
        set => SetValue(LeftTextProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating right button title.
    /// </summary>
    public string RightText
    {
        get => (string)GetValue(RightTextProperty);
        set => SetValue(RightTextProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating color of unselect state.
    /// Default is GreyNeutral3
    /// </summary>
    public Color UnselectedColor { get; set; } = ResourceHelper.GetColor("GreyNeutral3");

    /// <summary>
    /// Gets or sets a value indicating color of selected state.
    /// Default is White
    /// </summary>
    public Color SelectedColor { get; set; } = Colors.White;

    /// <summary>
    /// Gets or sets a value indicating color of border around button
    /// Default is GreyNeutral5
    /// </summary>
    public Color BorderColor { get; set; } = ResourceHelper.GetColor("GreyNeutral5");

    public StateButton()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Border.Stroke = BorderColor;
        Border.BackgroundColor = UnselectedColor;

        LeftSideLabel.Text = LeftText;
        RightSideLabel.Text = RightText;

        SelectStatePropertyChanged(SelectState);
    }

    public event EventHandler<SelectedState> OnStateChanged;
    private void LeftButtonOnTapped(object sender, TappedEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        OnStateChanged?.Invoke(this, SelectedState.Left);
        SelectState = SelectedState.Left;
    }

    private void RightButtonOnTapped(object sender, TappedEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        OnStateChanged?.Invoke(this, SelectedState.Right);
        SelectState = SelectedState.Right;
    }

    private void SelectStatePropertyChanged(SelectedState state)
    {
        if (state == SelectedState.Left)
        {
            SetLeftSideSelected();
        }
        else
        {
            SetRightSideSelected();
        }
    }

    private void SetLeftSideSelected()
    {
        LeftSideBorder.Stroke = BorderColor;
        LeftSideBorder.BackgroundColor = SelectedColor;

        DisableBorder.Stroke = Colors.Transparent;
        DisableBorder.BackgroundColor = UnselectedColor;
    }

    private void SetRightSideSelected()
    {
        DisableBorder.Stroke = BorderColor;
        DisableBorder.BackgroundColor = SelectedColor;

        LeftSideBorder.Stroke = Colors.Transparent;
        LeftSideBorder.BackgroundColor = UnselectedColor;
    }

    private static void OnSelectStatePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var stateButton = ((StateButton)bindable);
        stateButton.SelectStatePropertyChanged((SelectedState)newValue);
    }

    private static void OnLeftTextPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var stateButton = (StateButton)bindable;
        stateButton.LeftSideLabel.Text = (string)newValue;
    }

    private static void OnRightTextPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var stateButton = (StateButton)bindable;
        stateButton.RightSideLabel.Text = (string)newValue;
    }

    public enum SelectedState
    {
        Left,
        Right,
    }
}