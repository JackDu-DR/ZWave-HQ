using CommonLib.DTO.Implementation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZWave.Models
{
    public partial class MotionProfileModel : BaseModel
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private double _distance;

        [ObservableProperty]
        private double _time;

        [ObservableProperty]
        private double _velocity;

        [ObservableProperty]
        private double _acceleration;

        [ObservableProperty]
        private double _jerk;

        public override void LoadDataFromDTOJson(object baseDTO)
        {
            var data = (MotionProfileDTO)baseDTO;
            if (data != null)
            {
                Name = data.Name;
                Distance = data.Distance;
                Time = data.Time;
                Velocity = data.Velocity;
                Acceleration = data.Acceleration;
                Jerk = data.Jerk;
            }
        }
    }
}
