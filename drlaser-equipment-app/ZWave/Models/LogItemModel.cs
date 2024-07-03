namespace ZWave.Models
{
    public class LogItemModel : IndexedModel
    {
        public string CreatedTime { get; set; }

        public string Action { get; set; }

        public string Message { get; set; }

        public bool IsMachineLog { get; set; }

        public string UserName { get; set; }
    }
}
