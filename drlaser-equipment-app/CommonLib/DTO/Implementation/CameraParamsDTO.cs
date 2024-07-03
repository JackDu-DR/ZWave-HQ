using CommonLib.DTO.Interface;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLib.DTO.Implementation
{
    [Guid("3A97FD30-F719-4B3A-9EB2-7137C34E2DAD")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CameraParamsDTO : BaseDTO, ICameraParamsDTO
    {
        public double Exposure { get; set; }

        public bool Light { get; set; }

        public override void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<CameraParamsDTO>(json);

            Exposure = newObject.Exposure;
            Light = newObject.Light;
        }

        public byte[] ConvertToBytes(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
