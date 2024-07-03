using ZWave.Models;
using ZWave.Services.Interfaces;
using ZWave.Shared.Models;

namespace ZWave.Services
{
    public class AlarmGuidelineService : BaseService, IAlarmGuidelineService
    {
        private static readonly string ALARM_ENDPOINT = "api/AlarmGuideline";

        public async Task<IEnumerable<AlarmGuidelineModel>> GetAlarmGuides()
        {
            var alarmDtos = await HttpClientService.GetDataAsync<IEnumerable<AlarmGuidelineDto>>($"{ALARM_ENDPOINT}/All");
            var alarmModels = new List<AlarmGuidelineModel>();

            if (alarmDtos != null && alarmDtos.Count() > 0)
            {
                foreach (var alarmDto in alarmDtos)
                {
                    alarmModels.Add(new AlarmGuidelineModel(alarmDto.AlarmCode, alarmDto.Description, alarmDto.RecoveryGuideline));
                }
            }

            return alarmModels;
        }
    }
}
