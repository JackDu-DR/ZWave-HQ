using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("38DA8CFE-E4E4-443C-9271-3DBE7D2C2FBF")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionMovedDTO : IMotionMovedDTO
    {
        public double TransitionX { get; set; }
        public double TransitionY { get; set; }

        public void LoadDataFromJson(string json)
        {
            var motionMovedDto = JsonConvert.DeserializeObject<MotionMovedDTO>(json);

            TransitionX = motionMovedDto.TransitionX;
            TransitionY = motionMovedDto.TransitionY;
        }
    }
}
