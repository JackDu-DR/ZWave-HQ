using ZWave.ViewModels.Users;
using ZWave.Views.Users;

namespace ZWave.PopupBuilders
{
    public class TabPagePermissionPopupBuilder : BasePopupBuilder<TabPagePermissionPopup>
    {
        public override TabPagePermissionPopup Build()
        {
            return new TabPagePermissionPopup();
        }
    }
}
