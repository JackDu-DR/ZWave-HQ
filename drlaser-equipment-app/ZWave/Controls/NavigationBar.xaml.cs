using System.Windows.Input;
using ZWave.Shared.Enums;

namespace ZWave.Controls;

public partial class NavigationBar : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(NavigationBar), null);
    public static readonly BindableProperty SelectedTabPageProperty = BindableProperty.Create(nameof(SelectedTabPage), typeof(TabPage), typeof(NavigationBar), null, propertyChanged: OnSelectedTabPagePropertyChanged);

    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    public TabPage SelectedTabPage
    {
        get { return (TabPage)GetValue(SelectedTabPageProperty); }
        set { SetValue(SelectedTabPageProperty, value); }
    }

    public List<NavigationButton> NavigationButtons { get; set; }

    public NavigationBar()
    {
        NavigationButtons = new List<NavigationButton>();
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        if (NavigationButtons.Count > 0)
        {
            NavigationButtons[0].Selected = true;
            InitControl();
        }
    }

    private void InitControl()
    {
        NavigationButtons.ForEach(button =>
        {
            button.OnTappedHandler += OnButtonTapped;
            if (button.IsAtBarEnd)
            {
                LayoutEnd.Add(button);
            }
            else
            {
                Layout.Add(button);
            }
        });
    }

    private void OnButtonTapped(object sender, EventArgs e)
    {
        NavigationButtons.ForEach(button =>
        {
            if (button != sender)
            {
                button.Selected = false;
            }
        });

        if (Command != null)
        {
            var tabPage = (sender as NavigationButton).TabPage;
            Command.Execute(tabPage);
        }
    }

    private static void OnSelectedTabPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var navigateBar = (NavigationBar)bindable;
        var newTabPage = (TabPage)newValue;
        var selectedButton = navigateBar.NavigationButtons.FirstOrDefault(b => b.Selected);
        if (selectedButton == null || selectedButton.TabPage != newTabPage)
        {
            foreach (var button in navigateBar.NavigationButtons)
            {
                button.Selected = button.TabPage == newTabPage;
            }
        }
    }
}