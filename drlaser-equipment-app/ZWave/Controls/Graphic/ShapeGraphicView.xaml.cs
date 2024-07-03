using SkiaSharp;
using System.Diagnostics;
using System.Windows.Input;
using ZWave.Enums;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.Controls.Graphic;

public partial class ShapeGraphicView : ContentView
{
    private readonly int DELAY_COMMAND_EXECUTION_TIME = 100; //milliseconds

    public static readonly BindableProperty PointerChangedCommandProperty = BindableProperty.Create(nameof(PointerChangedCommand), typeof(ICommand), typeof(ShapeGraphicView), null);
    public static readonly BindableProperty CompleteDrawingCommandProperty = BindableProperty.Create(nameof(CompleteDrawingCommand), typeof(ICommand), typeof(ShapeGraphicView), null);
    public static readonly BindableProperty OnRotatingCommandProperty = BindableProperty.Create(nameof(OnRotatingCommand), typeof(ICommand), typeof(ShapeGraphicView), null);

    public static readonly BindableProperty StartingXYPositionValueProperty = BindableProperty.Create(nameof(StartingXYPositionValue), typeof(StartingXYPosition), typeof(ShapeGraphicView), propertyChanged: OnStartingXYPositionValuePropertyChanged);

    public static readonly BindableProperty ZoomValueProperty = BindableProperty.Create(nameof(ZoomValue), typeof(int), typeof(ShapeGraphicView), propertyChanged: OnZoomValuePropertyChanged, defaultValue: 1);
    public static readonly BindableProperty SelectedCameraNameProperty = BindableProperty.Create(nameof(SelectedCameraName), typeof(string), typeof(ShapeGraphicView), defaultValue: "ProsUpLookCam");

    public static readonly BindableProperty RatioProperty = BindableProperty.Create(nameof(Ratio), typeof(int), typeof(ShapeGraphicView), defaultValue: 1);
    public static readonly BindableProperty ResultROIsProperty = BindableProperty.Create(nameof(ResultROIs), typeof(IEnumerable<IShapeROI>), typeof(ShapeGraphicView), propertyChanged: OnResultROIsPropertyChanged);
    public static readonly BindableProperty ShapeTypeProperty = BindableProperty.Create(nameof(ShapeType), typeof(ShapeType), typeof(ShapeGraphicView), null);

    public static readonly BindableProperty DrawingEnabledProperty = BindableProperty.Create(nameof(DrawingEnabled), typeof(bool), typeof(ShapeGraphicView), propertyChanged: null, defaultValue: true);
	public static readonly BindableProperty ResetCanvasProperty = BindableProperty.Create(nameof(ResetCanvas), typeof(bool), typeof(ShapeGraphicView), defaultValue: false, Microsoft.Maui.Controls.BindingMode.TwoWay, propertyChanged: OnResetCanvasPropertyChanged);
    public class StartingXYPosition
    {
        public double StartingWidthValue { get; set; }
        public double StartingHeightValue { get; set; }
    }

    public ICommand PointerChangedCommand
    {
        get { return (ICommand)GetValue(PointerChangedCommandProperty); }
        set { SetValue(PointerChangedCommandProperty, value); }
    }

    public ICommand CompleteDrawingCommand
    {
        get { return (ICommand)GetValue(CompleteDrawingCommandProperty); }
        set { SetValue(CompleteDrawingCommandProperty, value); }
    }

    public ICommand OnRotatingCommand
    {
        get { return (ICommand)GetValue(OnRotatingCommandProperty); }
        set { SetValue(OnRotatingCommandProperty, value); }
    }

    public IEnumerable<IShapeROI> ResultROIs
    {
        get { return (IEnumerable<IShapeROI>)GetValue(ResultROIsProperty); }
        set { SetValue(ResultROIsProperty, value); }
    }

    public ShapeType ShapeType
    {
        get { return (ShapeType)GetValue(ShapeTypeProperty); }
        set { SetValue(ShapeTypeProperty, value); }
    }

    public bool DrawingEnabled
    {
        get { return (bool)GetValue(DrawingEnabledProperty); }
        set { SetValue(DrawingEnabledProperty, value); }
    }

    public string SelectedCameraName
    {
        get { return (string)GetValue(SelectedCameraNameProperty); }
        set { SetValue(SelectedCameraNameProperty, value); }
    }

    public StartingXYPosition StartingXYPositionValue
    {
        get { return (StartingXYPosition)GetValue(StartingXYPositionValueProperty); }
        set { SetValue(StartingXYPositionValueProperty, value); }
    }

    public int ZoomValue
    {
        get { return (int)GetValue(ZoomValueProperty); }
        set { SetValue(ZoomValueProperty, value); }
    }

    public int Ratio
    {
        get { return (int)GetValue(RatioProperty); }
        set { SetValue(RatioProperty, value); }
    }

    public bool ResetCanvas
    {
        get { return (bool)GetValue(ResetCanvasProperty); }
        set { SetValue(ResetCanvasProperty, value); }
    }

    public bool HasCenterAxisLines { get; set; } = false;

    private IEnumerable<IShapeROI> _shapeROIs;
    private SKColor _shapeColor = SKColors.Red;
    private SKColor _textColor = SKColors.White;
    private bool _centerPointVisible = true;
    private IShapeROI _shapeROIToRotate;
    private float _rotatingShapeROIPhi;

    public ShapeGraphicView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        if (HasCenterAxisLines)
        {
            CenterAxisGraphicView.InvalidateSurface();
        }
    }

    private void DrawCenterAxisLines(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        SKSurface surface = e.Surface;
        SKCanvas canvas = surface.Canvas;

        CanvasHelper.DrawCenterAxis(canvas, e.Info.Width, e.Info.Height);
    }

    public void ClearShapes()
    {
        _isDrawing = false;
        _shapeROIs = null;
        _shapeROIToRotate = null;
        _rotatingShapeROIPhi = 0;
        GraphicView.InvalidateSurface();
    }

    private bool _isDrawing;
    private Point? _startPoint;

    private void GraphicView_Touch(object sender, SkiaSharp.Views.Maui.SKTouchEventArgs e)
    {
        var position = new Point(Math.Floor(e.Location.X), Math.Floor(e.Location.Y));
        switch (e.ActionType)
        {
            case SkiaSharp.Views.Maui.SKTouchAction.Pressed:
#if ANDROID
                RunDelayedCommand(DELAY_COMMAND_EXECUTION_TIME, PointerChangedCommand, position);
#endif
                PointerPressed(position);
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Moved:
                PointerMoved(position);
#if WINDOWS
                PointerChangedCommand?.Execute(position);
                if (_shapeROIToRotate != null) 
                {
                    OnRotatingCommand?.Execute(GetNormalizedPhi(_shapeROIToRotate.Phi));
                }
#endif
                e.Handled = true;
                break;
            case SkiaSharp.Views.Maui.SKTouchAction.Released:
#if ANDROID
                if (_shapeROIToRotate != null)
                {
                    var newPhi = GetNormalizedPhi(_shapeROIToRotate.Phi);
                    RunDelayedCommand(DELAY_COMMAND_EXECUTION_TIME, OnRotatingCommand, newPhi);
                }
                RunDelayedCommand(DELAY_COMMAND_EXECUTION_TIME, PointerChangedCommand, position);
#endif
                PointerReleased(position);
                break;
        }
    }

    private void PointerPressed(Point? position)
    {
        if (!DrawingEnabled)
        {
            return;
        }

        _isDrawing = true;
        _startPoint = position;
        _shapeROIToRotate = GetShapeToRotate(position);
    }

    private void PointerMoved(Point? position)
    {
        if (_isDrawing)
        {
            if (_shapeROIToRotate != null)
            {
                _shapeROIToRotate.Phi = _rotatingShapeROIPhi + GetPhiBetweenPoints(_shapeROIToRotate.GetCenterPoint(), _startPoint, position);
            }
            else
            {
                _shapeColor = SKColors.Red;
                _centerPointVisible = true;

                IShapeROI shape = GetShape(_startPoint.Value, position.Value);
                _shapeROIs = new List<IShapeROI>() { shape };
            }

            GraphicView.InvalidateSurface();
        }
    }

    private void PointerReleased(Point? position)
    {
        if (!DrawingEnabled || _startPoint == null)
        {
            return;
        }

        if (_shapeROIToRotate != null)
        {
            _rotatingShapeROIPhi = 0;
            _shapeROIToRotate = null;
        }
        else
        {
            var shape = GetShape(_startPoint.Value, position.Value);
            _shapeColor = SKColors.Green;
            GraphicView.InvalidateSurface();
            RunDelayedCommand(DELAY_COMMAND_EXECUTION_TIME, CompleteDrawingCommand, shape);
        }
        _isDrawing = false;
        _startPoint = null;
    }

    private static void OnResetCanvasPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var graphView = (ShapeGraphicView)bindable;
        if ((bool)newValue)
        {
            graphView.ClearShapes();
            graphView.ResetCanvas = false;
        }
    }

    private static void OnResultROIsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var graphView = (ShapeGraphicView)bindable;
        if (graphView._isDrawing)
        {
            return;
        }

        graphView._shapeROIs = (IEnumerable<IShapeROI>)newValue;
        graphView._shapeColor = SKColors.Green;
        graphView._centerPointVisible = false;
        graphView.GraphicView.InvalidateSurface();
    }

    private IShapeROI GetShape(Point startPoint, Point endPoint)
    {
        switch (ShapeType)
        {
            case ShapeType.Rectangle:
                return CoordinatorToRectangle(startPoint, endPoint);
            case ShapeType.Ellipse:
                return CoordinatorToEllipse(startPoint, endPoint);
            case ShapeType.Circle:
            default:
                return CoordinatorToCircle(startPoint, endPoint);
        }
    }

    private static async void OnStartingXYPositionValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //if (!(newValue is StartingXYPosition startingXYPosition))
        //{
        //    throw new ArgumentException("New value must be of type StartingXYPosition");
        //}

        //var graphView = (ShapeGraphicView)bindable;

        //double startingWidthValue = startingXYPosition.StartingWidthValue;
        //double startingHeightValue = startingXYPosition.StartingHeightValue;
        //await Task.Delay(100); // give time to load ScrollToAsync, otherwise the scrolling result is accurate
        //await graphView.ScrollView.ScrollToAsync(startingWidthValue, startingHeightValue, true);
    }

    private static void OnZoomValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //var graphView = (ShapeGraphicView)bindable;
        //var zoomValue = (int)newValue > 0 ? (int)newValue : 1;

        //var MotionWidthValue = DimensionHelper.MotionVideoWidth;
        //var MotionHeightValue = DimensionHelper.MotionVideoHeight;

        //var FullImageWidthValue = DimensionHelper.FullImageWidth;
        //var FullImageHeightValue = DimensionHelper.FullImageHeight;

        ////if (graphView.SelectedCameraName != null)
        ////{
        ////    FullImageWidthValue = graphView.MaximumWidthValue;
        ////    FullImageHeightValue = graphView.MaximumHeightRequest;
        ////}

        //graphView.ScrollingArea.WidthRequest = FullImageWidthValue / zoomValue < MotionWidthValue ? MotionWidthValue : FullImageWidthValue / zoomValue;
        //graphView.ScrollingArea.HeightRequest = FullImageHeightValue / zoomValue < MotionHeightValue ? MotionHeightValue : FullImageHeightValue / zoomValue;
    }

    private Circle CoordinatorToCircle(Point startPoint, Point endPoint)
    {
        return new Circle()
        {
            CenterX = (float)(startPoint.X + endPoint.X) / 2,
            CenterY = (float)(startPoint.Y + endPoint.Y) / 2,
            Radius = (float)Math.Max(Math.Abs(_startPoint.Value.X - endPoint.X), Math.Abs(_startPoint.Value.Y - endPoint.Y)) / 2,
        };
    }

    private Rectangle CoordinatorToRectangle(Point startPoint, Point endPoint)
    {
        return new Rectangle()
        {
            StartX = (float)startPoint.X,
            StartY = (float)startPoint.Y,
            Width = (float)(endPoint.X - startPoint.X),
            Height = (float)(endPoint.Y - startPoint.Y),
        };
    }

    private Ellipse CoordinatorToEllipse(Point startPoint, Point endPoint)
    {
        return new Ellipse()
        {
            CenterX = (float)(startPoint.X + endPoint.X) / 2,
            CenterY = (float)(startPoint.Y + endPoint.Y) / 2,
            RadiusX = Math.Abs((float)(endPoint.X - startPoint.X) / 2),
            RadiusY = Math.Abs((float)(endPoint.Y - startPoint.Y) / 2),
        };
    }

    /// <summary>
    /// Event handler for PaintSurface event from SKCanvasView
    /// </summary>
    private void OnPaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        SKSurface surface = e.Surface;
        SKCanvas canvas = surface.Canvas;

        canvas.Clear();

        if (_shapeROIs == null)
        {
            return;
        }

        foreach (var shape in _shapeROIs)
        {
            CanvasHelper.DrawROITeachingShape(canvas, shape, _shapeColor, _centerPointVisible, _textColor, scoreFormatString: "{0}%");
        }
    }

    private IShapeROI GetShapeToRotate(Point? position)
    {
        if (_shapeROIs == null || !position.HasValue)
        {
            return null;
        }

        foreach (var shapeROI in _shapeROIs)
        {
            if (shapeROI.ContainsPoint(position.Value))
            {
                _rotatingShapeROIPhi = shapeROI.Phi;
                return shapeROI;
            }
        }

        return null;
    }

    private void RunDelayedCommand(int delayTime, ICommand command, object commandParams = null)
    {
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(delayTime), () => command?.Execute(commandParams));
    }

    #region Calculation
    private static float GetPhiBetweenPoints(Point? originCoordinatePoint, Point? startPoint, Point? endPoint)
    {
        if (!originCoordinatePoint.HasValue || !startPoint.HasValue || !endPoint.HasValue)
        {
            return 0;
        }
        var relativeX1 = startPoint.Value.X - originCoordinatePoint.Value.X;
        var relativeY1 = startPoint.Value.Y - originCoordinatePoint.Value.Y;
        var relativeX2 = endPoint.Value.X - originCoordinatePoint.Value.X;
        var relativeY2 = endPoint.Value.Y - originCoordinatePoint.Value.Y;
        // Use Math.Atan2 for accurate angle calculation (considering quadrants)
        var angle = (float)Math.Atan2(relativeY2, relativeX2) - (float)Math.Atan2(relativeY1, relativeX1);
        // Convert from radians to degrees
        angle = (float)(angle * 180f / Math.PI);
        // Adjust for negative angles
        if (angle < 0)
        {
            angle += 360f;
        }
        return angle;
    }

    private float GetNormalizedPhi(float phi)
    {
        return phi % 360;
    }
    #endregion
}