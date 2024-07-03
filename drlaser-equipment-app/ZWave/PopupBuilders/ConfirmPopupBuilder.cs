using ZWave.Views;

namespace ZWave.PopupBuilders
{
    public class ConfirmPopupBuilder : BasePopupBuilder<ConfirmPopup>
    {
        private string _title;
        private string _content;
        private string _confirmText;
        private string _cancelText;
        private Action _confirmAction;

        public ConfirmPopupBuilder(string title, string content, string confirmText, string cancelText, Action confirmAction)
        {
            _title = title;
            _content = content;
            _confirmText = confirmText;
            _cancelText = cancelText;
            _confirmAction = confirmAction;
        }

        public override ConfirmPopup Build()
        {
            return new ConfirmPopup(_confirmAction);
        }
    }
}
