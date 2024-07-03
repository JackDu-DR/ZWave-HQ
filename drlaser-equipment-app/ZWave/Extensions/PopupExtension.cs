using CommunityToolkit.Maui.Views;

namespace ZWave.Extensions
{
    public static class PopupExtension
    {
        public static readonly BindableProperty IsDisposedProperty =
            BindableProperty.CreateAttached(
                "IsDisposed",
                typeof(bool),
                typeof(Popup),
                default(bool));

        private static void SetIsDisposed(this Popup popup, bool isDispose)
        {
            popup.SetValue(IsDisposedProperty, isDispose);
        }

        private static bool IsDisposed(this Popup popup)
        {
            return (bool)popup.GetValue(IsDisposedProperty);
        }

        /// <summary>
        /// Open popup in current page, close it by ClosePopup instead of default Close to avoid unexpected error
        /// </summary>
        /// <param name="popup"></param>
        public static void OpenPopup(this Popup popup)
        {
            if (popup != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.ShowPopup(popup);
                });
            }
        }

        /// <summary>
        /// Close popup display in screen which open by OpenPopup
        /// </summary>
        /// <param name="popup"></param>
        public static void ClosePopup(this Popup popup)
        {
            if (popup != null && !popup.IsDisposed())
            {
                popup.SetIsDisposed(true);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    popup.Close();
                });
            }
        }
    }
}
