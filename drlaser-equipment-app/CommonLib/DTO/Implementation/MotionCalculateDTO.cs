using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("7417C284-B620-4F62-841A-4A7A7ACAB33F")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionCalculateDTO : IMotionCalculateDTO
    {
        public double PointOneEstTime { get; set; }
        public double PointTwoEstTime { get; set; }
        public double TotalEstCycleTime { get; set; }

        public void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<MotionCalculateDTO>(json);

            PointOneEstTime = newObject.PointOneEstTime;
            PointOneEstTime = newObject.PointOneEstTime;
            TotalEstCycleTime = newObject.TotalEstCycleTime;
        }
    }
}
