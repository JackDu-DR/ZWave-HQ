using System.Windows.Input;
using ZWave.Enums;

namespace ZWave.Controls;

public class DragTransition
{
    public double TransitionX { get; set; }
    public double TransitionY { get; set; }
}

public partial class DragDetector : ContentView
{
    private Point? _startPoint;

    public static readonly BindableProperty DragCompletedCommandProperty = BindableProperty.Create(nameof(DragCompletedCommand), typeof(ICommand), typeof(DragDetector), null);

    public ICommand DragCompletedCommand
    {
        get { return (ICommand)GetValue(DragCompletedCommandProperty); }
        set { SetValue(DragCompletedCommandProperty, value); }
    }

    public DragDetector()
    {
        InitializeComponent();
    }

    private void DragView_Touch(object sender, SkiaSharp.Views.Maui.SKTouchEventArgs e)
    {
        var position = new Point(Math.Floor(e.Location.X), Math.Floor(e.Location.Y));
        switch (e.ActionType)
        {
            case SkiaSharp.Views.Maui.SKTouchAction.Pressed:
                PointerPressed(position);
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Moved:
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Released:
                PointerReleased(position);
                break;
        }
    }

    private void PointerPressed(Point? position)
    {
        _startPoint = position;
        DragView.SetCustomCursor(CursorIcon.Hand, DragView.Handler?.MauiContext);
    }
    private void PointerReleased(Point? position)
    {
        if (_startPoint.HasValue && position.HasValue)
        {
            var distance = new DragTransition
            {
                TransitionX = position.Value.X - _startPoint.Value.X,
                TransitionY = position.Value.Y - _startPoint.Value.Y,
            };
            DragCompletedCommand?.Execute(distance);
        }
        DragView.SetCustomCursor(CursorIcon.Default, DragView.Handler?.MauiContext);
        _startPoint = null;
    }
}