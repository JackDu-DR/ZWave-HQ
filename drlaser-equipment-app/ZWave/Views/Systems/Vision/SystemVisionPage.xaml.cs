using ZWave.Controls;
using ZWave.Enums;
using ZWave.Helpers;
using ZWave.ViewModels.Systems.Vision;

namespace ZWave.Views;

public partial class SystemVisionPage : ContentView
{
    private SystemVisionPageViewModel _systemVisionPageViewModel;

    public SystemVisionPage(SystemVisionPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _systemVisionPageViewModel = vm;
    }

    public async void SaveImage(object sender, EventArgs e)
    {
        var contentType = LiveCameraContainer?.Content?.GetType();
        if (contentType == typeof(ImageFromStream))
        {
            var customImage = LiveCameraContainer.Content as ImageFromStream;
            var path = await FileHelper.ImageFileSaver(customImage.ImageStream);
            if (!string.IsNullOrEmpty(path))
            {
                FolderPath.Text = path;
            }
        }
    }

    public async void OpenImage(object sender, EventArgs e)
    {
        var result = await FileHelper.ImageFilePicker();

        if (result == null)
            return;

        FolderPath.Text = result.FullPath;
        OnLiveIndicator.IsVisible = false;
        var imageStream = await result.OpenReadAsync();
        LiveCameraContainer.Content = new ImageFromStream(imageStream);
    }

    private async void OnDiscardButtonClicked(object sender, EventArgs e)
    {
        ClearShapes();
    }

    private object _selectedShape;
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = (RadioButton)sender;
        if (e.Value && radioButton.Value != _selectedShape)
        {
            _selectedShape = radioButton.Value;
            ClearShapes();
        }
    }

    private void ShapeGraphicView_Scrolled(object sender, ScrolledEventArgs e)
    {
        ClearShapes();
        _systemVisionPageViewModel.ClearROI();
    }

    private void ShapeGraphicView_EndScrolled(object sender, Point e)
    {
        _systemVisionPageViewModel.Scroll(e.X, e.Y);
    }

    private void ClearShapes()
    {
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            if (ShapeGraphicView != null)
            {
                ShapeGraphicView.ClearShapes();
            }
        });
    }
}