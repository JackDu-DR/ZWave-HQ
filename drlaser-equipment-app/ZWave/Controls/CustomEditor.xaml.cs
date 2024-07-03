#if ANDROID
using ZWave.Helpers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Handlers;
using Android.Content.Res;
#endif

namespace ZWave.Controls;

public partial class CustomEditor : Border
{
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(CustomEditor), false);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEditor), defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEditor), defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomEditor), defaultBindingMode: BindingMode.TwoWay);

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public CustomEditor()
	{
		InitializeComponent();
#if ANDROID
        EditorBorder.Stroke = ResourceHelper.GetColor("GreyNeutral6");
        EditorHandler.Mapper.AppendToMapping("NoUnderlineEditor", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
        });
#endif
    }

    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
#if WINDOWS
    Editor.CursorPosition = Editor.Text.Length - 1;
    Editor.Focus();
#endif
    }
}