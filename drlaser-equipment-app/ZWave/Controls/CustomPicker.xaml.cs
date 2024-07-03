using System.Collections;

#if ANDROID
using ZWave.Helpers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Handlers;
using Android.Content.Res;
#endif

namespace ZWave.Controls;

public partial class CustomPicker : Border
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomPicker), string.Empty, propertyChanged: OnTitlePropertyChanged);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomPicker), default(double), BindingMode.TwoWay);
    public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding), typeof(string), typeof(CustomPicker), string.Empty, BindingMode.TwoWay, propertyChanged: OnItemDisplayBindingChanged);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CustomPicker), default(IList), BindingMode.TwoWay);
    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(CustomPicker), -1, BindingMode.TwoWay);
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(CustomPicker), null, BindingMode.TwoWay);

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    public string ItemDisplayBinding
    {
        get { return (string)GetValue(ItemDisplayBindingProperty); }
        set { SetValue(ItemDisplayBindingProperty, value); }
    }

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public int SelectedIndex
    {
        get { return (int)GetValue(SelectedIndexProperty); }
        set { SetValue(SelectedIndexProperty, value); }
    }
    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public CustomPicker()
	{
		InitializeComponent();
#if ANDROID
        PickerIcon.IsVisible = true;
        PickerHandler.Mapper.AppendToMapping("NoUnderlinePicker", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            h.PlatformView.SetPadding(5,0,5,0);
        });
#endif
    }

    private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((CustomPicker)bindable).PickerControl.Title = (string)newValue;
    }

    private static void OnItemDisplayBindingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((CustomPicker)bindable).PickerControl.ItemDisplayBinding = new Binding((string)newValue);
    }
}