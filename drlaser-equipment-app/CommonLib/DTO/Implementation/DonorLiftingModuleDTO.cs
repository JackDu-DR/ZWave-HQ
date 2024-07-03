using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("996FE9FF-EFE0-41EF-BF05-8313F0AA7C50")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class DonorLiftingModuleDTO : BaseDTO, IDonorLiftingModuleDTO
    {
        public double XAxisPosition { get; set; }
        public double YAxisPosition { get; set; }
        public double ZAxisPosition { get; set; }
        public bool IsDonorChuckVacuumEnabled { get; set; }
        public DonorLifterUIElement DonorLifterUIElement { get; set; }
        public MoveDirection MoveDirection { get; set; }
        public MotionCmd MotionCmd { get; set; }
        public CameraSelect CameraSelect { get; set; }
        public double MoveRel { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<DonorLiftingModuleDTO>(json);

            if (newObject == null) return;
            XAxisPosition = newObject.XAxisPosition;
            YAxisPosition = newObject.YAxisPosition;
            ZAxisPosition = newObject.ZAxisPosition;
            IsDonorChuckVacuumEnabled = newObject.IsDonorChuckVacuumEnabled;
            DonorLifterUIElement = newObject.DonorLifterUIElement;
            MoveDirection = newObject.MoveDirection;
            MotionCmd = newObject.MotionCmd;
            CameraSelect = newObject.CameraSelect;
            MoveRel = newObject.MoveRel;
        }
    }
}
