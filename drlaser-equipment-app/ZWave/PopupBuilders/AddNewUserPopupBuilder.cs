using ZWave.ViewModels.Users;
using ZWave.Views.Users;

namespace ZWave.PopupBuilders
{
    public class AddNewUserPopupBuilder : BasePopupBuilder<UserInfoPopup>
    {
        private readonly UserInfoPopupViewModel _userInfoPopupViewModel = Helpers.ServiceProvider.GetService<UserInfoPopupViewModel>();

        public override UserInfoPopup Build()
        {
            return new UserInfoPopup(_userInfoPopupViewModel);
        }
    }
}
