using Microsoft.Maui.Platform;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using System.Reflection;
using Windows.UI.Core;
using ZWave.Enums;

public static class CursorExtension
{
    public static void SetCustomCursor(this VisualElement visualElement, CursorIcon cursor, IMauiContext? mauiContext)
    {
        ArgumentNullException.ThrowIfNull(mauiContext);
        UIElement view = visualElement.ToPlatform(mauiContext);
        view.ChangeCursor(InputCursor.CreateFromCoreCursor(new CoreCursor(GetCursor(cursor), 1)));
    }

    static void ChangeCursor(this UIElement uiElement, InputCursor cursor)
    {
        Type type = typeof(UIElement);
        type.InvokeMember("ProtectedCursor", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, uiElement, new object[] { cursor });
    }

    static CoreCursorType GetCursor(CursorIcon cursor)
    {
        return cursor switch
        {
            CursorIcon.Hand => CoreCursorType.Hand,
            _ => CoreCursorType.Arrow,
        };
    }
}
