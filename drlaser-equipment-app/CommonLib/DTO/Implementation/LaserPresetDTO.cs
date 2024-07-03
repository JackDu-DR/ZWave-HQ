using CommonLib.DTO.Interface;
using CommonLib.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTO.Implementation
{
    [Guid("6C73A3AD-A724-4185-8026-81E0C1113BC3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]

    public class LaserPresetDTO : ILaserPresetDTO
    {
        public PresetControl presetIndex { get; set; }

        public void LoadDataFromJson(string json)
        {
            var newObject = JsonConvert.DeserializeObject<LaserPresetDTO>(json);

            presetIndex = newObject.presetIndex;
        }
    }
}
