using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("7D8CD593-6920-4BF4-99C4-E5DD5BD2AEC1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MasterDTO : BaseDTO, IMasterDTO
    {
        public string MessageContent { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<MasterDTO>(json);

            if (newObject == null) return;
            MessageContent = newObject.MessageContent;
        }
    }
}
