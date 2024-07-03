using ZWave.ViewModels.Users;
using ZWave.Views.Users;

namespace ZWave.PopupBuilders
{
    public class ChangePasswordPopupBuilder : BasePopupBuilder<ChangePasswordPopup>
    {
        private readonly ChangePasswordViewModel _changePasswordViewModel = Helpers.ServiceProvider.GetService<ChangePasswordViewModel>();
        private readonly Guid _userId;

        public ChangePasswordPopupBuilder(Guid userId)
        {
            _userId = userId;
        }

        public override ChangePasswordPopup Build()
        {
            return new ChangePasswordPopup(_changePasswordViewModel, _userId);
        }
    }
}
