namespace ZWave.PopupBuilders
{
    public class MachineConnectionLostPopupBuilder : BaseNeedShutdownErrorPopupBuilder
    {
        private static readonly string _title = Localization["MachineConnectionLostTitle"].ToString();
        private static readonly string _message = Localization["MachineConnectionLostContent"].ToString();

        public MachineConnectionLostPopupBuilder() : base(_title, _message)
        {
        }
    }
}
