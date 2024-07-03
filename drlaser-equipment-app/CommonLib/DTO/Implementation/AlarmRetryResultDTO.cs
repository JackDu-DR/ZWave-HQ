using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CommonLib.DTO.Implementation
{
    [Guid("4EDF6BBF-D887-4942-A2D9-8906D65B7AA0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class AlarmRetryResultDTO : IAlarmRetryResultDTO
    {
        public bool IsSuccess { get; set; }
        public string AlarmId { get; set; } = string.Empty;

        public void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<AlarmRetryResultDTO>(json);

            if (newObject != null)
            {
                IsSuccess = newObject.IsSuccess;
                AlarmId = newObject.AlarmId;
            }
        }
    }
}
