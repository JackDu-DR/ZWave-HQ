using Android.Views;
using Application = Android.App.Application;
using Microsoft.Maui.Platform;
using ZWave.Enums;

public static class CursorExtension
{
    public static void SetCustomCursor(this VisualElement visualElement, CursorIcon cursor, IMauiContext? mauiContext)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(24))
        {
            ArgumentNullException.ThrowIfNull(mauiContext);
            var view = visualElement.ToPlatform(mauiContext);
            view.PointerIcon = PointerIcon.GetSystemIcon(Application.Context, GetCursor(cursor));
        }
    }

    private static PointerIconType GetCursor(CursorIcon cursor)
    {
        return cursor switch
        {
            CursorIcon.Hand => PointerIconType.Hand,
            _ => PointerIconType.Default,
        };
    }
}
