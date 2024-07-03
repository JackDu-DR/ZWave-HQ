namespace ZWave.PopupBuilders
{
    public class SeverConnectionLostPopupBuilder : BaseNeedShutdownErrorPopupBuilder
    {
        private static readonly string _title = Localization["SeverConnectionLostTittle"].ToString();
        private static readonly string _message = Localization["SeverConnectionLostContent"].ToString();

        public SeverConnectionLostPopupBuilder() : base(_title, _message)
        {
        }
    }
}
