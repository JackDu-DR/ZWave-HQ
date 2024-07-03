using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("EBC9EDF1-27CC-4E40-A6AC-23AF480ECB37")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class ProcessTableDTO : BaseDTO, IProcessTableDTO
    {
        public double XAxisPosition { get; set; }
        public double YAxisPosition { get; set; }
        public double TXAxisPosition { get; set; }
        public double TYAxisPosition { get; set; }
        public double ZAxisPosition { get; set; }
        public bool IsSubstrateChuckVacuumEnabled { get; set; }
        public ProTableUIElement ProTableUIElement { get; set; }
        public MoveDirection MoveDirection { get; set; }
        public MotionCmd MotionCmd { get; set; }
        public CameraSelect CameraSelect { get; set; }
        public double MoveRel { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<ProcessTableDTO>(json);

            XAxisPosition = newObject.XAxisPosition;
            YAxisPosition = newObject.YAxisPosition;
            TXAxisPosition = newObject.TXAxisPosition;
            TYAxisPosition = newObject.TYAxisPosition;
            ZAxisPosition = newObject.ZAxisPosition;
            IsSubstrateChuckVacuumEnabled = newObject.IsSubstrateChuckVacuumEnabled;
            ProTableUIElement = newObject.ProTableUIElement;
            MoveDirection = newObject.MoveDirection;
            MotionCmd = newObject.MotionCmd;
            CameraSelect = newObject.CameraSelect;
            MoveRel = newObject.MoveRel;
        }
    }
}
