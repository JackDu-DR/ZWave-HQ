using ZWave.Views;

namespace ZWave.PopupBuilders
{
    public class EstablishConnectionErrorPopupBuilder
        : BasePopupBuilder<EstablishServerConnectionErrorPopup>
    {
        private readonly string _title;
        private readonly string _content;

        public EstablishConnectionErrorPopupBuilder()
        {
            _title = Localization["EstablishServerConnectionErrorTitle"].ToString();
            _content = Localization["EstablishServerConnectionErrorContent"].ToString();
        }

        public override EstablishServerConnectionErrorPopup Build()
        {
            return new EstablishServerConnectionErrorPopup(_title, _content);
        }
    }
}
