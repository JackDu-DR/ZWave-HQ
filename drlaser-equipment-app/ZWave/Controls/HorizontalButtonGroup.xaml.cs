namespace ZWave.Controls;

public partial class HorizontalButtonGroup : ContentView
{
    private ButtonInGroup _initialSelectedButton;

    public readonly BindableProperty SelectedButtonProperty = BindableProperty.Create(nameof(SelectedButton), typeof(ButtonInGroup), typeof(HorizontalButtonGroup), default, default);
    public readonly BindableProperty ButtonsProperty = BindableProperty.Create(nameof(Buttons), typeof(List<ButtonInGroup>), typeof(HorizontalButtonGroup), default, default);

    public ButtonInGroup SelectedButton
    {
        get => (ButtonInGroup)GetValue(SelectedButtonProperty);
        set => SetValue(SelectedButtonProperty, value);
    }
    public List<ButtonInGroup> Buttons
    {
        get => (List<ButtonInGroup>)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    public bool ResetOnUnload { get; set; } = false;

    public HorizontalButtonGroup()
	{
        Buttons = new List<ButtonInGroup>();
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
	}

    private void OnUnloaded(object sender, EventArgs e)
    {
        Unloaded -= OnUnloaded;
        if (ResetOnUnload)
        {
            Reset();
        }
    }

    private void Reset()
    {
        foreach (var button in Buttons)
        {
            button.IsSelected = false;
        }
        _initialSelectedButton.IsSelected = true;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        if (Buttons.Count == 0)
        {
            return;
        }

        SelectedButton.IsSelected = true;
        _initialSelectedButton = SelectedButton;

        foreach (var button in Buttons)
        {
            button.OnButtonSelected += OnButtonSelected;
            ButtonContainer.Add(button);
        }
    }

    private void OnButtonSelected(object sender, EventArgs e)
    {
        SelectedButton = (ButtonInGroup)sender;
        var unselectedButtons = Buttons.Where(b => b != SelectedButton);
        foreach (var button in unselectedButtons)
        {
            if (button.IsSelected)
            {
                button.IsSelected = false;
            }
        }
    }
}