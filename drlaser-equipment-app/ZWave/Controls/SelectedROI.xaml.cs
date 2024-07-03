using Microsoft.Maui;
using ZWave.Models;

namespace ZWave.Controls;

public partial class SelectedROI : ContentView
{
    public static readonly BindableProperty ShapeROIProperty = BindableProperty.Create(nameof(ShapeROI), typeof(IShapeROI), typeof(SelectedROI), propertyChanged: OnShapeROIPropertyChange);

    public static readonly BindableProperty ScrollXProperty = BindableProperty.Create(nameof(ScrollX), typeof(double), typeof(SelectedROI), propertyChanged: null);
    public static readonly BindableProperty ScrollYProperty = BindableProperty.Create(nameof(ScrollX), typeof(double), typeof(SelectedROI), propertyChanged: null);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomPicker), default(double), BindingMode.TwoWay);

    public IShapeROI ShapeROI
    {
        get { return (IShapeROI)GetValue(ShapeROIProperty); }
        set { SetValue(ShapeROIProperty, value); }
    }

    public double ScrollX
    {
        get { return (double)GetValue(ScrollXProperty); }
        set { SetValue(ScrollXProperty, value); }
    }

    public double ScrollY
    {
        get { return (double)GetValue(ScrollYProperty); }
        set { SetValue(ScrollYProperty, value); }
    }

    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    public SelectedROI()
    {
        InitializeComponent();
    }

    private static void OnShapeROIPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        var ROIResult = (SelectedROI)bindable;
        if (newValue == null)
        {
            ROIResult.CircleContainer.IsVisible = false;
            ROIResult.RectangleContainer.IsVisible = false;
            ROIResult.EllipseContainer.IsVisible = false;
        }
        else if (newValue is Circle cicle)
        {
            ROIResult.CircleContainer.IsVisible = true;
            ROIResult.RectangleContainer.IsVisible = false;
            ROIResult.EllipseContainer.IsVisible = false;

            ROIResult.CircleRowCenter.Text = cicle.CenterX.ToString();
            ROIResult.CircleColumnCenter.Text = cicle.CenterY .ToString();
            ROIResult.CircleRadius.Text = cicle.Radius.ToString();
        }
        else if (newValue is Rectangle rectangle)
        {
            ROIResult.CircleContainer.IsVisible = false;
            ROIResult.RectangleContainer.IsVisible = true;
            ROIResult.EllipseContainer.IsVisible = false;

            var row1 = rectangle.Width < 0 ? rectangle.StartX + rectangle.Width : rectangle.StartX;
            var column1 = rectangle.Height < 0 ? rectangle.StartY + rectangle.Height : rectangle.StartY;
            var row2 = rectangle.Width < 0 ? rectangle.StartX : rectangle.StartX + rectangle.Width;
            var column2 = rectangle.Height < 0 ? rectangle.StartY : rectangle.StartY + rectangle.Height;
            var rowCenter = row1 + rectangle.Width / 2 ;
            var columnCenter = column1 + rectangle.Height / 2;

            ROIResult.RectangleRow1.Text = row1.ToString();
            ROIResult.RectangleColumn1.Text = column1.ToString();
            ROIResult.RectangleRow2.Text = row2.ToString();
            ROIResult.RectangleColumn2.Text = column2.ToString();
            ROIResult.RectangleRowCenter.Text = rowCenter.ToString();
            ROIResult.RectangleColumnCenter.Text = columnCenter.ToString();
            ROIResult.RectanglePhi.Text = rectangle.Phi.ToString();
        }
        else if (newValue is Ellipse ellipse)
        {
            ROIResult.CircleContainer.IsVisible = false;
            ROIResult.RectangleContainer.IsVisible = false;
            ROIResult.EllipseContainer.IsVisible = true;

            var rowCenter = ellipse.CenterX;
            var columnCenter = ellipse.CenterY;
            var row1 = rowCenter - ellipse.RadiusX;
            var column1 = columnCenter - ellipse.RadiusY;
            var radius1 = ellipse.RadiusX;
            var radius2 = ellipse.RadiusY;

            ROIResult.EllipseRow1.Text = row1.ToString();
            ROIResult.EllipseColumn1.Text = column1.ToString();
            ROIResult.EllipseRowCenter.Text = rowCenter.ToString();
            ROIResult.EllipseColumnCenter.Text = columnCenter.ToString();
            ROIResult.EllipseRadius1.Text = radius1.ToString();
            ROIResult.EllipseRadius2.Text = radius2.ToString();
            ROIResult.EllipsePhi.Text = ellipse.Phi.ToString();
        }
    }
}