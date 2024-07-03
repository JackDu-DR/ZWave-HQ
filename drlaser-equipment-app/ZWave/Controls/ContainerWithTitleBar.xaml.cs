namespace ZWave.Controls;

public partial class ContainerWithTitleBar : ContentView
{
    public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(nameof(TitleText), typeof(string), typeof(ContainerWithTitleBar), string.Empty);

    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }

    public ContainerWithTitleBar()
	{
		InitializeComponent();
	}
}