using ZWave.Views;

namespace ZWave.PopupBuilders
{
    public abstract class BaseNeedShutdownErrorPopupBuilder : BasePopupBuilder<NeedShutdownErrorPopup>
    {
        private string _title;
        private string _message;

        public BaseNeedShutdownErrorPopupBuilder(string title, string message)
        {
            _title = title;
            _message = message;
        }

        public override NeedShutdownErrorPopup Build()
        {
            return new NeedShutdownErrorPopup(_title, _message);
        }
    }
}
