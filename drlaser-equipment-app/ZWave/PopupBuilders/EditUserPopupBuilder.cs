using ZWave.ViewModels.Users;
using ZWave.Views.Users;

namespace ZWave.PopupBuilders
{
    public class EditUserPopupBuilder : BasePopupBuilder<UserInfoPopup>
    {
        private readonly UserInfoPopupViewModel _userInfoPopupViewModel = Helpers.ServiceProvider.GetService<UserInfoPopupViewModel>();
        private readonly Guid _userId;

        public EditUserPopupBuilder(Guid userId)
        {
            _userId = userId;
        }

        public override UserInfoPopup Build()
        {
            return new UserInfoPopup(_userInfoPopupViewModel, _userId);
        }
    }
}
