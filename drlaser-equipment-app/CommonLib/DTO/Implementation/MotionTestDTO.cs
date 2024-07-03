using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("AD024818-2046-485B-9178-F7628784E603")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionTestDTO : IMotionTestDTO
    {
        public string Name { get; set; }
        public double PointOne { get; set; }
        public double PointOneDelay { get; set; }
        public double PointTwo { get; set; }
        public double PointTwoDelay { get; set; }
        public double NoOfCycles { get; set; }
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
        public double Jerk { get; set; }

        public void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<MotionTestDTO>(json);

            Name = newObject.Name;
            PointOne = newObject.PointOne;
            PointOneDelay = newObject.PointOneDelay;
            PointTwo = newObject.PointTwo;
            PointTwoDelay = newObject.PointTwoDelay;
            NoOfCycles = newObject.NoOfCycles;
            Velocity = newObject.Velocity;
            Acceleration = newObject.Acceleration;
            Jerk = newObject.Jerk;
        }
    }
}
