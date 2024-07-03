using ZWave.Controls;

namespace ZWave.PopupBuilders
{
    public abstract class BaseInformationBoxPopupBuilder : BasePopupBuilder<InformationBoxPopup>
    {
        private string _title;
        private string _message;

        public BaseInformationBoxPopupBuilder(string title, string message)
        {
            _title = title;
            _message = message;
        }

        public override InformationBoxPopup Build()
        {
            return new InformationBoxPopup(_title, _message);
        }
    }
}
