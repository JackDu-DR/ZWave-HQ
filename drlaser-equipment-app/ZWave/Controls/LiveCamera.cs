using ZWave.Helpers;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace ZWave.Controls
{
    public class LiveCamera : SKCanvasView
    {
        public static readonly BindableProperty CameraSourceProperty = BindableProperty.Create(nameof(CameraSource), typeof(byte[]), typeof(LiveCamera), default, propertyChanged: OnCameraSourceChanged);
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(LiveCamera), default);

        //
        // Summary:
        //     Gets or sets the source of the Live Camera. This is a bindable property.
        //
        // Value:
        //     An struct.System.Byte array representing the Live Camera source. Default
        //     is null.
        //
        // Remarks:
        //     To be added.
        public byte[] CameraSource
        {
            get => (byte[])GetValue(CameraSourceProperty);
            set => SetValue(CameraSourceProperty, value);
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        private static void OnCameraSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var liveCamera = bindable as LiveCamera;

            if (liveCamera.IsActive)
            {
                ((LiveCamera)bindable).InvalidateSurface();
            }
            else
            {
                //TODO: put logic here (if any) when the LiveCamera is inactive,
                //such as storing the latest newValue...
            }
        }

        private static bool _isLoading;
        protected override async void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;
            base.OnPaintSurface(e);

            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            CanvasHelper.DrawImageBytes(canvas, CameraSource, (float)Width, (float)Height);
            await Task.Delay((int)(1000/ DeviceDisplay.Current.MainDisplayInfo.RefreshRate));

#if ANDROID
            if (CameraSource != null)
            {
                var generation  = GC.GetGeneration(CameraSource);
                GC.Collect(generation, GCCollectionMode.Forced);
            }

#endif
            _isLoading = false;
        }
    }
}

//using CommonLib.ClassHierarchy.Interface;
//using ZWave.Services;

//namespace ZWave.Controls
//{
//    public class LiveCamera : Image
//    {
//        public static readonly BindableProperty CameraSourceProperty = BindableProperty.Create(nameof(CameraSource), typeof(byte[]), typeof(LiveCamera), default, propertyChanged: OnCameraSourceChanged);
//        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(LiveCamera), default);

//        //
//        // Summary:
//        //     Gets or sets the source of the Live Camera. This is a bindable property.
//        //
//        // Value:
//        //     An struct.System.Byte array representing the Live Camera source. Default
//        //     is null.
//        //
//        // Remarks:
//        //     To be added.
//        public byte[] CameraSource
//        {
//            get => (byte[])GetValue(CameraSourceProperty);
//            //set => SetValue(CameraSourceProperty, value);
//            set
//            {
//                SetValue(CameraSourceProperty, value);
//                //this.RefreshIsEnabledProperty();
//                //this.IsVisible = false;
//                //this.IsVisible = true;
//                //if(LogService == null)
//                //LogService = Helpers.ServiceProvider.GetService<ILogService>();
//            }
//        }


//        //
//        // Summary:
//        //     Gets or sets a value indicating whether this LiveCamera is active in the user interface.
//        //     This is a bindable property.
//        //
//        // Value:
//        //     true if the element is active; otherwise, false. The default value is true
//        //
//        // Remarks:
//        //     LiveCameras that are not active will not set the source when CameraSource changed.
//        //     The following example shows a handler on parent view's OnDissappearing which will then set IsActive
//        //     to false on the LiveCamera.
//        //     public partial class MasterPage : ContentPage
//        //     {
//        //         protected override void OnAppearing()
//        //         {
//        //             base.OnAppearing();
//        //             liveCamera.IsActive = true;
//        //         }

//        //         protected override void OnDisappearing()
//        //         {
//        //             base.OnDisappearing();
//        //             liveCamera.IsActive = false;
//        //         }
//        //     }
//        public bool IsActive
//        {
//            get => (bool)GetValue(IsActiveProperty);
//            set => SetValue(IsActiveProperty, value);
//        }

//        private static void OnCameraSourceChanged(BindableObject bindable, object oldValue, object newValue)
//        {
//            //only do the setting Source when the LiveCamera is active to user
//            var liveCamera = bindable as LiveCamera;
//            var sourceBytes = newValue as byte[];

//            if (liveCamera.IsActive)
//            {
//                SetSourceAsync(liveCamera, sourceBytes);
//            }
//            else
//            {
//                //TODO: put logic here (if any) when the LiveCamera is inactive,
//                //such as storing the latest newValue...
//            }
//        }

//        private static void SetSourceAsync(LiveCamera liveCamera, byte[] newValue)
//        {
//            if (liveCamera.IsLoading)
//            {
//                return;
//            }
//            MainThread.BeginInvokeOnMainThread(() =>
//            {
//                if (newValue == null)
//                {
//                    //liveCamera.Source = null;
//                }
//                else

//                    liveCamera.Source = ImageSource.FromStream(() => new BufferedStream(new MemoryStream(newValue)));
//            });
//        }
//    }
//}

