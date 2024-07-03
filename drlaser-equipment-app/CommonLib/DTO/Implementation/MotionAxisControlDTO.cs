using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("3C8AE85D-80D1-4F53-8B98-F3600E1D4D64")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MotionAxisControlDTO : BaseDTO, IMotionAxisControlDTO
    {
        public double MovePos { get; set; }
        public double VelPos { get; set; }
        public double AcclPos { get; set; }
        public double JerkPos { get; set; }
        public double ActualMovePos { get; set; }
        public double ActualVelPos { get; set; }
        public double ActualAcclPos { get; set; }
        public double ActualJerkPos { get; set; }
        public bool IsRelPos { get; set; }
        public bool EnableBtnIsActive { get; set; }
        public MotionUIElement MotionUIElement { get; set; }
        public MotionUIElement ExecutingBtn { get; set; }
        public AxisSelection AxisSelection { get; set; }
        public MotionCmd MotionCmd { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<MotionAxisControlDTO>(json);
            MovePos = newObject.MovePos;
            VelPos = newObject.VelPos;
            AcclPos = newObject.AcclPos;
            JerkPos = newObject.JerkPos;
            ActualMovePos = newObject.ActualMovePos;
            ActualVelPos = newObject.ActualVelPos;
            ActualAcclPos = newObject.ActualAcclPos;
            ActualJerkPos = newObject.ActualJerkPos;
            IsRelPos = newObject.IsRelPos;
            EnableBtnIsActive = newObject.EnableBtnIsActive;
            MotionUIElement = newObject.MotionUIElement;
            ExecutingBtn = newObject.MotionUIElement;
            AxisSelection = newObject.AxisSelection;
            MotionCmd = newObject.MotionCmd;
        }
    }
}
