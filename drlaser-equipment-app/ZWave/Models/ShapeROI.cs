namespace ZWave.Models
{
    public interface IShapeROI
    {
        float Phi { get; set; }
        float? Score { get; set; }

        Point GetCenterPoint();
        bool ContainsPoint(Point point);
    }
}
