using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("D5A1BEAB-D5B1-4792-8FBE-378C014006C7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Message : BaseDTO, IMessage
    {
        public string Id { get; set; }
        public object Data { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var message = JsonConvert.DeserializeObject<Message>(json);

            Id = message.Id;
            Data = message.Data;
        }
    }
}
