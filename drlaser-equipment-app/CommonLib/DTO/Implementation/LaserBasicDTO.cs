using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("18879276-F0E2-47D1-BE2E-9BEAEA08E638")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserBasicDTO : LaserApplyDTO, ILaserBasicDTO
    {
        public bool IsOutputEnabled { get; set; }

        public override void LoadDataFromJson(string json)
        {
            base.LoadDataFromJson(json);

            var newObject = JsonConvert.DeserializeObject<LaserBasicDTO>(json);

            IsOutputEnabled = newObject.IsOutputEnabled;
        }
    }
}
