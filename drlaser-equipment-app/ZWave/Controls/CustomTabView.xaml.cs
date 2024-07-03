using System.Collections.ObjectModel;
using static ZWave.Controls.CustomTabItem;

namespace ZWave.Controls;

public partial class CustomTabView : ContentView
{
    public readonly BindableProperty TabItemsProperty = BindableProperty.Create(nameof(TabItems), typeof(Collection<CustomTabItem>), typeof(CustomTabView), new Collection<CustomTabItem>(), propertyChanged: OnTabItemsChanged);
    public readonly BindableProperty CurrentItemProperty = BindableProperty.Create(nameof(CurrentItem), typeof(CustomTabItem), typeof(CustomTabView), default, default);

    /// <summary>
    /// Get or Set The currently selected DRLaserEquipmentApp.Controls.CustomTabItem
    /// </summary>
    public CustomTabItem CurrentItem
    {
        get => (CustomTabItem)GetValue(CurrentItemProperty);
        set => SetValue(CurrentItemProperty, value);
    }

    public Collection<CustomTabItem> TabItems
    {
        get => (Collection<CustomTabItem>)GetValue(TabItemsProperty);
        set
        {
            SetValue(TabItemsProperty, value);
            AddTabItems(value);
        }
    }

    public Color ContentBackgroundColor { get; set; } = Colors.White;

    private static void OnTabItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customTabView = (CustomTabView)bindable;
        customTabView.TabItems = (Collection<CustomTabItem>)newValue;
    }

    private void AddTabItems(Collection<CustomTabItem> tabItems)
    {
        TabBarItemContainer.Children.Clear();
        if (tabItems.Any())
        {
            foreach (CustomTabItem child in tabItems)
            {
                child.OnTappedHandler -= OnTabItemSelected;
                child.OnTappedHandler += OnTabItemSelected;
                TabBarItemContainer.Children.Add(child);
            }
        }
    }

    private void OnTabItemSelected(object sender, EventArgs e)
    {
        CurrentItem = (CustomTabItem)sender;
        SetTabItemsState(TabItems, CurrentItem);
        SetTabView(CurrentItem.TabType);
    }

    private void SetTabView(TabType tabType)
    {
        var view = TabTypeResolver.TabTypeToView(tabType);

        TabContentView.Content = view;
    }

    private void SetTabItemsState(Collection<CustomTabItem> tabItems, CustomTabItem currentTabItem)
    {
        if (TabItems.Any())
        {
            foreach (CustomTabItem item in TabItems)
            {
                item.State = CustomTabItem.TabItemState.Normal;
            }
        }

        currentTabItem.State = CustomTabItem.TabItemState.Selected;
    }

    public CustomTabView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        TabContentView.BackgroundColor = ContentBackgroundColor;
        AddTabItems(TabItems);
        SetTabView(CurrentItem.TabType);
        SetTabItemsState(TabItems, CurrentItem);
        SetTabView(CurrentItem.TabType);
        Loaded -= OnLoaded;
    }
}