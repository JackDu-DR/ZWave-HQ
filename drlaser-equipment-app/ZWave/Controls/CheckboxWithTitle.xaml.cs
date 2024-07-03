namespace ZWave.Controls;

public partial class CheckboxWithTitle : HorizontalStackLayout
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CheckboxWithTitle), string.Empty, propertyChanged: OnTitlePropertyChange);
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckboxWithTitle), false, BindingMode.TwoWay, propertyChanged: OnIsCheckedPropertyChange);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public CheckboxWithTitle()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        TitleLabel.Text = Title;
        Checkbox.IsChecked = IsChecked;
    }

    private static void OnTitlePropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((CheckboxWithTitle)bindable).TitleLabel.Text = (string)newValue;
    }

    private static void OnIsCheckedPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        ((CheckboxWithTitle)bindable).IsChecked = (bool)newValue;
        ((CheckboxWithTitle)bindable).Checkbox.IsChecked = (bool)newValue;
    }

    public event EventHandler OnCheckedChanged;
    private void OnChanged(object sender, CheckedChangedEventArgs e)
    {
        OnCheckedChanged?.Invoke(this, e);
        IsChecked = e.Value;
    }
}