using ZWave.Models;

namespace ZWave.Services.Interfaces
{
    public interface IAlarmGuidelineService
    {
        Task<IEnumerable<AlarmGuidelineModel>> GetAlarmGuides();
    }
}
