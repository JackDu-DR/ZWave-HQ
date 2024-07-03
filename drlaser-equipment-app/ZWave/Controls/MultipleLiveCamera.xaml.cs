using System.Collections;
using ZWave.ViewModels.Jobs;

namespace ZWave.Controls;

public partial class MultipleLiveCamera : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(MultipleLiveCamera), default(IList), BindingMode.OneWay);

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MultipleLiveCamera), null, BindingMode.TwoWay, null, OnSelectedItemPropertyChanged);

    public static readonly BindableProperty Camera1SourceProperty = BindableProperty.Create(nameof(Camera1Source), typeof(byte[]), typeof(MultipleLiveCamera));
    public static readonly BindableProperty Camera2SourceProperty = BindableProperty.Create(nameof(Camera2Source), typeof(byte[]), typeof(MultipleLiveCamera));
    public static readonly BindableProperty Camera3SourceProperty = BindableProperty.Create(nameof(Camera3Source), typeof(byte[]), typeof(MultipleLiveCamera));

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public byte[] Camera1Source
    {
        get { return (byte[])GetValue(Camera1SourceProperty); }
        set { SetValue(Camera1SourceProperty, value); }
    }

    public byte[] Camera2Source
    {
        get { return (byte[])GetValue(Camera2SourceProperty); }
        set { SetValue(Camera2SourceProperty, value); }
    }

    public byte[] Camera3Source
    {
        get { return (byte[])GetValue(Camera3SourceProperty); }
        set { SetValue(Camera3SourceProperty, value); }
    }

    public MultipleLiveCamera()
    {
        InitializeComponent();
    }

    private static void OnSelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var multipleLiveCameras = (MultipleLiveCamera)bindable;
        var cameraOption = Enum.Parse<CameraOption>((string)newValue);

        if (cameraOption == CameraOption.All)
        {
            multipleLiveCameras.Grid4Cameras.ColumnDefinitions = new ColumnDefinitionCollection() { new ColumnDefinition() { Width = GridLength.Star }, new ColumnDefinition() { Width = GridLength.Star } };
            multipleLiveCameras.Grid4Cameras.RowDefinitions = new RowDefinitionCollection() { new RowDefinition() { Height = GridLength.Star }, new RowDefinition() { Height = GridLength.Star } };

            multipleLiveCameras.Camera1.IsVisible = true;
            multipleLiveCameras.Camera2.IsVisible = true;
            multipleLiveCameras.Camera3.IsVisible = true;
        }
        else
        {
            multipleLiveCameras.Grid4Cameras.ColumnDefinitions = new ColumnDefinitionCollection() { new ColumnDefinition() { Width = GridLength.Star } };
            multipleLiveCameras.Grid4Cameras.RowDefinitions = new RowDefinitionCollection() { new RowDefinition() { Height = GridLength.Star } };
            multipleLiveCameras.Camera1.IsVisible = false;
            multipleLiveCameras.Camera2.IsVisible = false;
            multipleLiveCameras.Camera3.IsVisible = false;

            switch (cameraOption)
            {
                case CameraOption.Camera1:
                    multipleLiveCameras.Camera1.IsVisible = true;
                    break;
                case CameraOption.Camera2:
                    multipleLiveCameras.Camera2.IsVisible = true;
                    break;
                case CameraOption.Camera3:
                    multipleLiveCameras.Camera3.IsVisible = true;
                    break;
            }
        }
    }
}