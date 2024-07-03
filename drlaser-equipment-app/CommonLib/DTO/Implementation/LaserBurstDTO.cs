using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("78299917-3846-4249-9718-CD94E2FC7E1B")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class LaserBurstDTO : LaserBurstApplyDTO, ILaserBurstDTO
    {
        public bool IsPowerlockEnabled { get; set; }

        public override void LoadDataFromJson(string json)
        {
            base.LoadDataFromJson(json);

            var newObject = JsonConvert.DeserializeObject<LaserBurstDTO>(json);

            IsPowerlockEnabled = newObject.IsPowerlockEnabled;
        }
    }
}
